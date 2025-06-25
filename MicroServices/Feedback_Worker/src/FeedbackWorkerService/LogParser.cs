using System.Globalization;
using System.Text.RegularExpressions;
using Feedback.ShapeInfoManagement.Dto;

using FeedBack.ProgramInfoManagement.Dto;
using FeedbackWorkerService;
using Volo.Abp.DependencyInjection;

public class LogParser : ISingletonDependency
{
    private readonly ILogger<LogParser> _logger;

    private readonly IConfiguration _configuration;

    public LogParser(ILogger<LogParser> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<LogModel> ParseAsync(string filePath, CancellationToken cancellationToken)
    {
        var result = new LogModel();
        _logger.LogInformation("Parsing log file: {file}", filePath);

        try
        {
            await Task.Delay(300, cancellationToken); // 等待文件写入完成

            var lines = await File.ReadAllLinesAsync(filePath, cancellationToken);
            if (!lines.Any(line => line.Contains("EDB-Verification finished")))
            {
                result.Valid = false;
                _logger.LogInformation("Invalid log file: {file}", filePath);
                return result;
            }

            result.Valid = true;
            ParseLines(filePath, lines, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing log file: {file}", filePath);
        }

        return result;
    }

    public LogModel Parse(string filePath)
    {
        var result = new LogModel();
        _logger.LogInformation("Parsing log file: {file}", filePath);

        try
        {
            var lines = File.ReadAllLines(filePath);
            if (!lines.Any(line => line.Contains("EDB-Verification finished")))
            {
                result.Valid = false;
                _logger.LogInformation("Invalid log file: {file}", filePath);
                return result;
            }

            _logger.LogInformation("EDB-Verification finished");
            result.Valid = true;
            ParseLines(filePath, lines, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing log file: {file}", filePath);
        }

        return result;
    }

    private void ParseLines(string filePath, string[] lines, LogModel result)
    {
        result.Program.Name = filePath.Split('\\').Last().Split('_').First();
        result.Program.Line = _configuration.GetValue<string>("AoiInfo:Line", "Line00");
        // result.Program.Line = "Line45"; // TODO:从config文件读取Line信息
        
        _logger.LogInformation("Program name: {name}", result.Program.Name);

        result.Program.Date = ParseDate(filePath.Split('\\').Last().Split('.').First().Split('_').Last());

        var keyLines = lines.SkipWhile(l => !l.Contains("Window-based EDB-Verification started"))
                            .TakeWhile(l => !l.Contains("Window-based EDB-Verification finished"))
                            .Select(l => l.Trim())
                            .ToList();

        _logger.LogInformation("Found {count} key lines", keyLines.Count);

        CreateOrUpdateShapeInfoDto? currentShape = null;

        foreach (var line in keyLines)
        {
            if (line.Contains("Macro successful tested:"))
            {
                currentShape = new CreateOrUpdateShapeInfoDto
                {
                    Name = line.Split(':').Last().Trim(),
                    // Line = result.Program.Line
                };
                result.ShapeInfos.Add(currentShape);
                _logger.LogDebug("Shape created: {shape}", currentShape.Name);
            }
            else if (line.Contains("Error:") && currentShape != null)
            {
                currentShape.Date = ParseDateTime(line);
                currentShape.GoodWindows = 0;
                currentShape.SlipWindows = 0;
                currentShape.FailureWindows = 0;
                currentShape.HasError = true;
                _logger.LogDebug("Shape has error: {shape}", currentShape.Name);
            }
            else if (line.Contains("Good Windows:") && currentShape != null)
            {
                var parts = line.Split(new[] { "Good Windows:", "Slip Windows:", "Failure Windows:" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 3)
                {
                    currentShape.Date = ParseDateTime(line);
                    currentShape.GoodWindows = int.Parse(parts[1].Trim());
                    currentShape.SlipWindows = int.Parse(parts[2].Trim());
                    currentShape.FailureWindows = int.Parse(parts[3].Trim());
                    _logger.LogDebug("Shape updated: {shape}", currentShape.Name);
                }
            }
            else if (line.Contains("Good Windows verified"))
            {
                result.Program.Good = ExtractLastInt(line);
            }
            else if (line.Contains("slip windows verified"))
            {
                result.Program.Slip = ExtractLastInt(line);
            }
            else if (line.Contains("failure windows verified"))
            {
                result.Program.Failure = ExtractLastInt(line);
            }
        }
    }

    private DateTime ParseDateTime(string line)
    {
        var datePart = line.Substring(0, 23);
        return DateTime.ParseExact(datePart, "dd.MM.yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
    }

    private int ExtractLastInt(string line)
    {
        var lastPart = line.Split(':').Last().Trim();
        var numberPart = lastPart.Split(' ').First().Trim();
        return int.TryParse(numberPart, out var value) ? value : 0;
    }
    

    /// <summary>
    /// 解析日期06052025145617285
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private DateTime ParseDate(string input)
    {
        string day = input.Substring(0, 2);
        string month = input.Substring(2, 2);
        string year = input.Substring(4, 4);
        string hour = input.Substring(8, 2);
        string minute = input.Substring(10, 2);
        string second = input.Substring(12, 2);
        string millisecond = input.Substring(14, 3);

        string dateTimeString = $"{year}-{month}-{day} {hour}:{minute}:{second}.{millisecond}";

        if (DateTime.TryParseExact(
                dateTimeString,
                "yyyy-MM-dd HH:mm:ss.fff",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime result))
        {
            Console.WriteLine("转换后的时间: " + result.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }
        else
        {
            Console.WriteLine("解析失败");
        }

        return result;
    }
}
