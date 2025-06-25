using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Feedback.ShapeInfoManagement.Dto
{
    public class CreateOrUpdateShapeInfoDto: EntityDto<Guid?>
    {
        
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 程序
        /// </summary>
        public Guid ProgramId { get; set; }
        
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
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }
        
    }
}