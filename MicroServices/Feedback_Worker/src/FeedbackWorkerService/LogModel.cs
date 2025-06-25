using Feedback.ShapeInfoManagement.Dto;
using FeedBack.ProgramInfoManagement.Dto;

public class LogModel
{
    public bool Valid { get; set; } = false;

    public CreateOrUpdateProgramInfoDto Program { get; set; } = new CreateOrUpdateProgramInfoDto();

    public List<CreateOrUpdateShapeInfoDto> ShapeInfos { get; set; } = new List<CreateOrUpdateShapeInfoDto>(); //CreateOrUpdateShapeInfoDto shapes { get; set; }

}