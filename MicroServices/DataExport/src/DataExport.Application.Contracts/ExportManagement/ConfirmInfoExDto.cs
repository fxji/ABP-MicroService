using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace DataExport.ExportManagement
{
    public class ConfirmInfoExDto : EntityDto<Guid?>
    {

        /// <summary>
        /// UserId
        /// </summary>
        public Guid? CreatorId { get; set; }

        /// <summary>
        /// A3Id
        /// </summary>
        [Required]
        public Guid? A3Id { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [Required]
        public string Comments { get; set; }

    }
}