using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AAA.A3MemberManagement.Dto
{
    public class CreateOrUpdateA3MemberDto: EntityDto<Guid?>
    {
        public Guid? A3Id { get; set; }

        /// <summary>
        /// Member
        /// </summary>
        [Required]
        public string UserId { get; set; }
        
        /// <summary>
        /// Organization
        /// </summary>
        [Required]
        public string OrganizationId { get; set; }
        
    }
}