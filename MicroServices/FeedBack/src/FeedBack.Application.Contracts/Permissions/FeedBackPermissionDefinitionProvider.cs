using FeedBack.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace FeedBack.Permissions;

public class FeedBackPermissionDefinitionProvider : PermissionDefinitionProvider
{
    // public override void Define(IPermissionDefinitionContext context)
    // {
    //     var myGroup = context.AddGroup(FeedBackPermissions.GroupName, L("Permission:FeedBack"));
    // }

    public override void Define(IPermissionDefinitionContext context)
    {
        var FeedBack = context.AddGroup(FeedBackPermissions.FeedBack, L("Permission:FeedBack"));

        var ProgramInfo = FeedBack.AddPermission(FeedBackPermissions.ProgramInfo.Default, L("ProgramInfo"));
        ProgramInfo.AddChild(FeedBackPermissions.ProgramInfo.Update, L("Edit"));
        ProgramInfo.AddChild(FeedBackPermissions.ProgramInfo.Delete, L("Delete"));
        ProgramInfo.AddChild(FeedBackPermissions.ProgramInfo.Create, L("Create"));

        var ShapeInfo = FeedBack.AddPermission(FeedBackPermissions.ShapeInfo.Default, L("ShapeInfo"));
        ShapeInfo.AddChild(FeedBackPermissions.ShapeInfo.Update, L("Edit"));
        ShapeInfo.AddChild(FeedBackPermissions.ShapeInfo.Delete, L("Delete"));
        ShapeInfo.AddChild(FeedBackPermissions.ShapeInfo.Create, L("Create"));

        //Code generation...
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FeedBackResource>(name);
    }
}
