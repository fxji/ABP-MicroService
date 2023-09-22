using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.A3Management.Dto
{
    public class CreateOrUpdateA3Dto : EntityDto<Guid?>
    {

        /// <summary>
        /// Location/Plant/Site
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Sponsor / Champion
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 开关
        /// </summary>
        public bool ReOccurrence { get; set; }

        /// <summary>
        /// 日期选择
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Process of Production issue
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// Reference A3 
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Source Of Defect
        /// </summary>
        public Guid SourceId { get; set; }

    }
}