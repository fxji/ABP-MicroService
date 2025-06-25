using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Feedback.ShapeInfoManagement.Dto
{
    public class ShapeInfoDto : EntityDto<Guid?>
    {

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 程序
        /// </summary>
        public Guid ProgramId { get; set; }


        public string ProgramName { get; set; }

        /// <summary>
        /// HasError
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        /// SlipWindows
        /// </summary>
        public int SlipWindows { get; set; }

        /// <summary>
        /// GoodWindows
        /// </summary>
        public int GoodWindows { get; set; }

        /// <summary>
        /// HasChange
        /// </summary>
        public bool HasChanged { get; set; }

        /// <summary>
        /// FailureWindows
        /// </summary>
        public int FailureWindows { get; set; }

        /// <summary>
        /// 产线
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        // public string PersonInCharge { get; set; }
        

        public DateTime Date { get; set; }

        
    }
}