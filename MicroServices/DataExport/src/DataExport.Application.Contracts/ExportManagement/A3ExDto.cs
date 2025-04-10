using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace DataExport.ExportManagement
{
    public class A3ExDto : EntityDto<Guid?>
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

        public string UserEmail { get; set; }

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
        public string Process { get; set; }

        /// <summary>
        /// Reference A3 
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Source Of Defect
        /// </summary>
        public string Source { get; set; }

    }
}