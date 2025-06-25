using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace FeedBack.ProgramInfoManagement.Dto
{
    public class CreateOrUpdateProgramInfoDto : EntityDto<Guid?>
    {

        /// <summary>
        /// 产线
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 程序
        /// </summary>
        public string Name { get; set; }

        public int Good { get; set; }

        public int Failure { get; set; }

        public int Slip { get; set; }
        
        public DateTime Date { get; set; }

        
    }
}