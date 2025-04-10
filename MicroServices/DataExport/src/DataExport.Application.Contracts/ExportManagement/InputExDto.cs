using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DataExport.ExportManagement
{
    public class InputExDto : EntityDto<Guid?>
    {
        public A3ExDto A3 { get; set; }

        public List<IssueExDto> Issues { get; set; }

        public List<ContainmentActionExDto> ContainmentActions { get; set; }

        public List<RiskAssessmentExDto> RiskAssessments { get; set; }

        public List<CauseExDto> Causes { get; set; }

        public List<CorrectiveActionExDto> CorrectiveActions { get; set; }

        public List<ConfirmInfoExDto> ConfirmInfos { get; set; }

        public List<A3MemberExDto> A3Members { get; set; }

        public List<A3AttachmentExDto> A3Attachments { get; set; }
    }
}
