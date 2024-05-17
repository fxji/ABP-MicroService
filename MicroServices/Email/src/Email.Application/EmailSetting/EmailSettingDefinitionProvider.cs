using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Emailing;
using Volo.Abp.Settings;

namespace Email.EmailSetting
{
    public class EmailSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(EmailSettingNames.Smtp.Host, "SMTPHubEU.contiwan.com"),
                new SettingDefinition(EmailSettingNames.Smtp.Port, "587"),
                new SettingDefinition(EmailSettingNames.Smtp.UserName, "uie80936@contiwan.com"),
                new SettingDefinition(EmailSettingNames.Smtp.Password, "conti@cc2024"),
                new SettingDefinition(EmailSettingNames.Smtp.EnableSsl, "false"),
                new SettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials,"false"),
                new SettingDefinition(EmailSettingNames.DefaultFromAddress, "fengxiang.ji@continental-corporation.com"),
                new SettingDefinition(EmailSettingNames.DefaultFromDisplayName, "system message")
            );
        }
    }
}
