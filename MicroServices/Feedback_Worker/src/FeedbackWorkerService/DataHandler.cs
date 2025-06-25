using System.Threading.Tasks;
using Feedback.ShapeInfoManagement;
using Feedback.ShapeInfoManagement.Dto;
using FeedBack.ProgramInfoManagement;
using FeedBack.ProgramInfoManagement.Dto;
using Volo.Abp.DependencyInjection;

public class DataHandler : IScopedDependency
{
    private readonly LogParser _logParser;

    private IProgramInfoAppService _programInfoAppService;

    private IShapeInfoAppService _shapeInfoAppService;

    public DataHandler(LogParser logParser, IProgramInfoAppService programInfoAppService, IShapeInfoAppService shapeInfoAppService)
    {
        _logParser = logParser;
        _programInfoAppService = programInfoAppService;
        _shapeInfoAppService = shapeInfoAppService;
    }
    

    // 添加http异常，超时，重试
    public async Task Handle(LogModel result)
    {
        if (!result.Valid)
        {
            return;
        }

        var input = new GetProgramInfoInputDto()
        {
            Name = result.Program.Name,
            Line = result.Program.Line
        };

        var latestProgramInfo = await _programInfoAppService.GetLatestByNameAndLine(input);

        bool programInfoChanged = false;
        // latestProgramInfo == null ||
        //     latestProgramInfo.Good + latestProgramInfo.Failure + latestProgramInfo.Slip !=
        //     result.Program.Good + result.Program.Failure + result.Program.Slip;

        // 第一次读到日志信息时，hasChanged = false
        if (latestProgramInfo != null)
        {
            if (latestProgramInfo.Good + latestProgramInfo.Failure + latestProgramInfo.Slip !=
                result.Program.Good + result.Program.Failure + result.Program.Slip)
            {
                programInfoChanged = true;
            }
        }

        var programInfo = await _programInfoAppService.DataPost(new CreateOrUpdateProgramInfoDto
        {
            Name = result.Program.Name,
            Line = result.Program.Line,
            Date = result.Program.Date,
            Good = result.Program.Good,
            Slip = result.Program.Slip,
            Failure = result.Program.Failure,
        });

        foreach (var shapeInfo in result.ShapeInfos)
        {
            bool shapeInfoChanged = programInfoChanged;
            if (shapeInfoChanged)
            {
                var input2 = new GetShapeInfoInputDto()
                {
                    Name = shapeInfo.Name,
                    Line = result.Program.Line
                };
                var latestShapeInfo = await _shapeInfoAppService.GetLatestByNameAndLine(input2);

                shapeInfoChanged = latestShapeInfo == null ||
                    latestShapeInfo.GoodWindows + latestShapeInfo.FailureWindows + latestShapeInfo.SlipWindows !=
                    shapeInfo.GoodWindows + shapeInfo.FailureWindows + shapeInfo.SlipWindows;
            }

            await _shapeInfoAppService.DataPost(new CreateOrUpdateShapeInfoDto
            {
                Name = shapeInfo.Name,
                Date = shapeInfo.Date,
                GoodWindows = shapeInfo.GoodWindows,
                SlipWindows = shapeInfo.SlipWindows,
                FailureWindows = shapeInfo.FailureWindows,
                HasChanged = shapeInfoChanged,
                ProgramId = programInfo.Id.Value,
                Line = result.Program.Line,
                HasError = shapeInfo.HasError
            });
        }
    }
}