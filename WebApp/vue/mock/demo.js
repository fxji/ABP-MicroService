
const tokens = {
  admin: {
    token: 'admin-token'
  },
  editor: {
    token: 'editor-token'
  }
}
export default [
  // login, permission, role.
  //   // implement the code for the TODO comment
  {
    url: '/connect/token',
    type: 'post',
    response: request => {
      const { username, password } = request.body
      const token = tokens[username]

      // mock error
      if (!token) {
        return {
          code: 400,
          message: 'Login failed, please check your username and password.'
        }
      }

      return {
        code: 20000,
        access_token: token.token
      }
    }
  },
  // implement the code for the TODO comment
  {
    url: '/api/abp/application-configuration',
    type: 'get',
    response: _ => {
      return {
        code: 20000,
        auth: {
          grantedPolicies: {
            'BaseService.DataDictionary': true,
            'BaseService.DataDictionary.Update': true,
            'BaseService.DataDictionary.Delete': true,
            'BaseService.DataDictionary.Create': true,
            'BaseService.Menu': true,
            'BaseService.Menu.Update': true,
            'BaseService.Menu.Delete': true,
            'BaseService.Menu.Create': true,
            'BaseService.Organization': true,
            'BaseService.Organization.Update': true,
            'BaseService.Organization.Delete': true,
            'BaseService.Organization.Create': true,
            'BaseService.Job': true,
            'BaseService.Job.Update': true,
            'BaseService.Job.Delete': true,
            'BaseService.Job.Create': true,
            'BaseService.AuditLogging': true,
            'AbpIdentity.Roles': true,
            'AbpIdentity.Roles.Create': true,
            'AbpIdentity.Roles.Update': true,
            'AbpIdentity.Roles.Delete': true,
            'AbpIdentity.Roles.ManagePermissions': true,
            'AbpIdentity.Users': true,
            'AbpIdentity.Users.Create': true,
            'AbpIdentity.Users.Update': true,
            'AbpIdentity.Users.Delete': true,
            'AbpIdentity.Users.ManagePermissions': true,
            'AbpTenantManagement.Tenants': true,
            'AbpTenantManagement.Tenants.Create': true,
            'AbpTenantManagement.Tenants.Update': true,
            'AbpTenantManagement.Tenants.Delete': true,
            'AbpTenantManagement.Tenants.ManageFeatures': true,
            'AbpTenantManagement.Tenants.ManageConnectionStrings': true,
            'FeatureManagement.ManageHostFeatures': true,
            'StorageManagement.File': true,
            'StorageManagement.File.Delete': true,
            'StorageManagement.File.Update': true,
            'StorageManagement.File.Create': true,
            'Business.Book': true,
            'Business.Book.Create': true,
            'Business.Book.Update': true,
            'Business.Book.Delete': true,
            'Business.PrintTemplate': true,
            'Business.PrintTemplate.Create': true,
            'Business.PrintTemplate.Update': true,
            'Business.PrintTemplate.Delete': true,
            'FormManagement.Form': true,
            'FormManagement.Form.Update': true,
            'FormManagement.Form.Delete': true,
            'FormManagement.Form.Create': true,
            'FormManagement.FormBuild': true,
            'FormManagement.FormBuild.Download': true,
            'FormManagement.FormBuild.Build': true,
            'FormManagement.FormBuild.Update': true,
            'FlowManagement.WorkFlow': true,
            'FlowManagement.WorkFlow.DoWorkFlow': true,
            'FlowManagement.WorkFlow.Create': true,
            'FlowManagement.Flow': true,
            'FlowManagement.Flow.Delete': true,
            'FlowManagement.Flow.Create': true,
            'FlowManagement.Flow.Update': true,
            'AAA.A3': true,
            'AAA.A3.Delete': true,
            'AAA.A3.Create': true,
            'AAA.A3.Update': true,
            'AAA.Issue': true,
            'AAA.Issue.Create': true,
            'AAA.Issue.Delete': true,
            'AAA.Issue.Update': true,
            'AAA.RiskAssessment': true,
            'AAA.RiskAssessment.Delete': true,
            'AAA.RiskAssessment.Update': true,
            'AAA.RiskAssessment.Create': true,
            'AAA.CorrectiveAction': true,
            'AAA.CorrectiveAction.Update': true,
            'AAA.CorrectiveAction.Create': true,
            'AAA.CorrectiveAction.Delete': true,
            'AAA.Cause': true,
            'AAA.Cause.Create': true,
            'AAA.Cause.Delete': true,
            'AAA.Cause.Update': true,
            'AAA.ContainmentAction': true,
            'AAA.ContainmentAction.Delete': true,
            'AAA.ContainmentAction.Update': true,
            'AAA.ContainmentAction.Create': true,
            'AAA.A3Attachment': true,
            'AAA.A3Attachment.Create': true,
            'AAA.A3Attachment.Update': true,
            'AAA.A3Attachment.Delete': true,
            'AAA.A3Member.Update': true,
            'AAA.A3Member.Delete': true,
            'AAA.A3Member.Create': true,
            'AAA.A3Member': true,
            'PeBusiness.PeTask': true,
            'PeBusiness.PeTask.Delete': true,
            'PeBusiness.PeTask.Create': true,
            'PeBusiness.PeTask.Update': true,
            'Blogging.Blog': true,
            'Blogging.Blog.Delete': true,
            'Blogging.Blog.Management': true,
            'Blogging.Blog.Update': true,
            'Blogging.Blog.Create': true,
            'Blogging.Blog.ClearCache': true,
            'Blogging.Tag': true,
            'Blogging.Tag.Delete': true,
            'Blogging.Tag.Create': true,
            'Blogging.Tag.Update': true,
            'Blogging.Comment': true,
            'Blogging.Comment.Delete': true,
            'Blogging.Comment.Update': true,
            'Blogging.Comment.Create': true,
            'Blogging.Post': true,
            'Blogging.Post.Create': true,
            'Blogging.Post.Delete': true,
            'Blogging.Post.Update': true,
            'ProductManagement.Product': true,
            'ProductManagement.Product.Update': true,
            'ProductManagement.Product.Delete': true,
            'ProductManagement.Product.Create': true
          }
        }
      }
    }
  },
  // get user menus
  {
    url: '/api/base/role-menus',
    type: 'get',
    response: _ => {
      return {
        code: 20000,
        items: [
        ]
      }
    }
  },
  // mock for api/feedback/failedinfo, return SerialNo PieceNo Project ComId ComType MaterialNo Quantity Position IsTop FailureMode
  {
    url: '/api/feedback/failedinfo',
    type: 'get',
    response: _ => {
      return {
        code: 20000,
        items: [
          {
            SerialNo: 'SN123456',
            PieceNo: 'PN78910',
            Project: 'ProjectX',
            ComId: 332,
            ComType: 2,
            MaterialNo: 'MN45678',
            Quantity: 100,
            Position: 'Warehouse1',
            IsTop: true,
            FailureMode: 'ModeB'
          }
        ]
      }
    }
  },
  {
    url: 'api/feedback/failedinfo/:feedbackId',
    type: 'get',
    response: _ => {
      return {
        code: 20000,
        items: [
        ]
      }
    }

  },
  // mock for api/EDBVerify/ShapeCheckInfo, return name, programid, line, failurewindows,goodwindows,slipwindows,haschanged,cause,createitme,personincharge,
  {
    url: '/api/EDBVerify/ShapeCheckInfo',
    type: 'get',
    response: _ => {
      return {
        code: 20000,
        items: [
          {
            Id: 1,
            Name: 'BGA_A2C02980600_GENR_18_0',
            ProgramId: 'A3C1269140092',
            Line: 'line1',
            FailureWindows: 0,
            GoodWindows: 4,
            SlipWindows: 0,
            HasChanged: false,
            Cause: 1,
            CreateTime: new Date().toLocaleString(),
            PersonInCharge: 'personincharge1'
          },
          {
            Id: 2,
            Name: '0402_C_LAND_2_0',
            ProgramId: 'A3C1269140092',
            Line: 'line1',
            FailureWindows: 0,
            GoodWindows: 5,
            SlipWindows: 0,
            HasChanged: true,
            Cause: 1,
            CreateTime: new Date().toLocaleString(),
            PersonInCharge: 'personincharge1'
          },
          {
            Id: 3,
            Name: '0402_C_MENI_9_0',
            ProgramId: 'A3C1269140092',
            Line: 'line1',
            FailureWindows: 0,
            GoodWindows: 31,
            SlipWindows: 0,
            HasChanged: false,
            Cause: 1,
            CreateTime: new Date().toLocaleString(),
            PersonInCharge: 'personincharge1'
          }
        ]
      }
    }
  },
  // mock for api/edbverify/programcheckinfo, return name, programid, line, failurewindows,goodwindows,slipwindows,haschanged,cause,createitme,personincharge,
  {
    url: '/api/EDBVerify/ProgramCheckInfo',
    type: 'get',
    response: _ => {
      return {
        code: 20000,
        items: [
          {
            Id: 1,
            Name: 'A3C1269140092',
            Line: 'line1',
            FailureWindows: 0,
            GoodWindows: 4,
            SlipWindows: 0,
            CreateTime: new Date().toLocaleString(),
            PersonInCharge: 'personincharge1'
          },
          {
            Id: 2,
            Name: 'A3C1269140093',
            Line: 'line1',
            FailureWindows: 0,
            GoodWindows: 5,
            SlipWindows: 0,
            CreateTime: new Date().toLocaleString(),
            PersonInCharge: 'personincharge1'
          },
          {
            Id: 3,
            Name: 'A3C1269140095',
            Line: 'line1',
            FailureWindows: 0,
            GoodWindows: 31,
            SlipWindows: 0,
            CreateTime: new Date().toLocaleString(),
            PersonInCharge: 'personincharge1'
          }
        ]
      }
    }
  }
]
