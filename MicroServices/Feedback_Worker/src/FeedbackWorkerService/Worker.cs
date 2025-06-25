using System.Threading.Channels;
using FeedBack.Samples;

namespace FeedbackWorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private readonly LogParser _logParser;

    private readonly DataHandler _dataHandler;

    private readonly Channel<string> _fileQueue = Channel.CreateUnbounded<string>();

    private FileSystemWatcher _watcher = null!;

    private readonly IConfiguration _configuration;


    public Worker(
        ILogger<Worker> logger,
        LogParser parser,
        DataHandler dataHandler,
        IConfiguration configuration
        )
    {
        _logger = logger;
        _logParser = parser;
        _dataHandler = dataHandler;
        _configuration = configuration;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _watcher = new FileSystemWatcher
        {
            Path = _configuration.GetValue<string>("FileWatcher:Path", Directory.GetCurrentDirectory()),
            Filter = _configuration.GetValue<string>("FileWatcher:Filter", "*.log"),
            NotifyFilter = NotifyFilters.FileName,
            EnableRaisingEvents = _configuration.GetValue<bool>("FileWatcher:EnableRaisingEvents", true),
            // Path = Directory.GetCurrentDirectory(),
            // Filter = "*.log",
            // NotifyFilter = NotifyFilters.FileName,
            // EnableRaisingEvents = true,
        };

        _watcher.Created += OnChanged;
        // _watcher.Changed += OnChanged;

        _logger.LogInformation("File watcher started.");

        return base.StartAsync(cancellationToken);
    }


    //当调用此方法时，会提示文件被占用，所以改为channel
    private void ProcessFile(object sender, FileSystemEventArgs e)
    {
        var result = _logParser.Parse(e.FullPath);
        _logger.LogInformation("program name: {result}", result.Program.Name);
        _logger.LogInformation("program pictures: {result}", result.Program.Good);
        _logger.LogInformation("shapes count: {file}", result.ShapeInfos.Count);
        // throw new NotImplementedException();
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        _logger.LogWarning("File changed: {file}", e.Name);
        _fileQueue.Writer.TryWrite(e.FullPath);
    }



    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started.");


        // 模式一，文件队列
        while (await _fileQueue.Reader.WaitToReadAsync(stoppingToken))
        {
            while (_fileQueue.Reader.TryRead(out var filePath))
            {
                var result = await _logParser.ParseAsync(filePath, stoppingToken);

                //TODO: 保存到数据库

               await _dataHandler.Handle(result);
            }
        }

        // // 模式二，定时扫描
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     _logger.LogInformation("开始扫描...");

        //     // await _monitorService.ScanLogsAndDetectChangesAsync();

        //     _logger.LogInformation("等待下次扫描...");
        //     await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);  // 每5分钟扫描一次
        // }



    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _watcher.Dispose();
        return base.StopAsync(cancellationToken);
    }
}
