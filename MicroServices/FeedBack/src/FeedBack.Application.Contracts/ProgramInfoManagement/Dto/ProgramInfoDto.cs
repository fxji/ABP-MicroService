using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace FeedBack.ProgramInfoManagement.Dto
{
    public class ProgramInfoDto : EntityDto<Guid?>
    {

        /// <summary>
        /// 产线
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 程序
        /// </summary>
        public string Name { get; set; }

        public string Windows { get; set; }

        public int Slip { get; set; }

        public int Good { get; set; }

        public int Failure { get; set; }
        
        public DateTime Date { get; set; }
        
    }
}