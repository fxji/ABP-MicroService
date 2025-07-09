using System.Threading.Channels;
using FeedBack.Samples;

namespace FeedbackWorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private readonly LogParser _logParser;

    private readonly DataHandler _dataHandler;

    private readonly Channel<string> _fileQueue = Channel.CreateUnbounded<string>();

    private readonly Dictionary<string, Timer> _debounceTimers = new();
    private readonly TimeSpan _debounceDelay = TimeSpan.FromMinutes(5);

    private FileSystemWatcher _watcher = null!;

    private readonly IConfiguration _configuration;


    public Worker(
        ILogger<Worker> logger,
        LogParser parser,
        DataHandler dataHandler,
        IConfiguration configuration
        )
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logParser = parser ?? throw new ArgumentNullException(nameof(parser));
        _dataHandler = dataHandler ?? throw new ArgumentNullException(nameof(dataHandler));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        //从配置文件读取延迟时间
        if (!int.TryParse(_configuration.GetValue<string>("AoiInfo:DebounceDelay"), out var debounceDelayMinutes))
        {
            debounceDelayMinutes = 3;
        }

        _debounceDelay = TimeSpan.FromMinutes(debounceDelayMinutes);
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
{
    try
    {
        var path = _configuration.GetValue<string>("FileWatcher:Path", Directory.GetCurrentDirectory());
        var filter = _configuration.GetValue<string>("FileWatcher:Filter", "*.log");
        var enableRaisingEvents = _configuration.GetValue<bool>("FileWatcher:EnableRaisingEvents", true);

        if (string.IsNullOrWhiteSpace(path))
        {
            throw new InvalidOperationException("FileWatcher path cannot be null or empty.");
        }

        _watcher = new FileSystemWatcher
        {
            Path = path,
            Filter = filter,
            NotifyFilter = NotifyFilters.FileName,
            EnableRaisingEvents = enableRaisingEvents,
        };

        _watcher.Created += OnChanged;

        _logger.LogInformation("File watcher started.");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error starting file watcher.");
        throw;
    }

    await base.StartAsync(cancellationToken);
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
        await foreach (var filePath in _fileQueue.Reader.ReadAllAsync(stoppingToken))
        {
            // 去抖动逻辑：每个文件路径对应一个 Timer
            if (_debounceTimers.TryGetValue(filePath, out var existingTimer))
            {
                // 重置计时器（延迟再处理）
                existingTimer.Change(_debounceDelay, Timeout.InfiniteTimeSpan);
            }
            else
            {
                // Create a new Timer for handling the file processing with a debounce delay
                var timer = new Timer(_ =>
                {
                    // Use Task.Run to execute the file processing asynchronously
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            // Check if the file is ready for processing
                            if (IsFileReady(filePath))
                            {
                                // Parse the log file asynchronously and get the result
                                var result = await _logParser.ParseAsync(filePath, stoppingToken);

                                // Handle the parsed result, typically involves saving to the database
                                await _dataHandler.Handle(result);
                            }
                            else
                            {
                                // If the file is still being used, requeue it for later processing
                                _fileQueue.Writer.TryWrite(filePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log any exceptions that occur during the file processing
                            _logger.LogError(ex, "Error parsing log file: {file}", filePath);
                        }
                        finally
                        {
                            // Ensure the debounce timer is removed from the dictionary once processing is complete
                            _debounceTimers.Remove(filePath);
                        }

                    });

                }, null, _debounceDelay, Timeout.InfiniteTimeSpan);

                // Store the timer in the dictionary with the file path as the key for future reference
                _debounceTimers[filePath] = timer;
            }
        }
        // 文件长时间写不完，被占用
        // 模式一，文件队列
        // while (await _fileQueue.Reader.WaitToReadAsync(stoppingToken))
        // {
        //     while (_fileQueue.Reader.TryRead(out var filePath))
        //     {
        //         try
        //         {
        //             var result = await _logParser.ParseAsync(filePath, stoppingToken);

        //             //TODO: 保存到数据库

        //             await _dataHandler.Handle(result);
        //         }
        //         catch (Exception ex)
        //         {
        //             _logger.LogError(ex, "Error parsing log file: {file}", filePath);
        //         }
        //     }
        // }

        // // 模式二，定时扫描
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     _logger.LogInformation("开始扫描...");

        //     // await _monitorService.ScanLogsAndDetectChangesAsync();

        //     _logger.LogInformation("等待下次扫描...");
        //     await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);  // 每5分钟扫描一次
        // }



    }

    private bool IsFileReady(string path)
    {
        try
        {
            using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _watcher.Dispose();
        return base.StopAsync(cancellationToken);
    }
}
