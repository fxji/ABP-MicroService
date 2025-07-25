﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AAA.A3Management.Dto
{
    public class ShareDto : EntityDto<Guid?>
    {
        /// <summary>
        /// multiple email join with ','
        /// </summary>
        public string EmailAddress { get; set; }
        
        public CreateOrUpdateA3Dto A3 { get; set; }
    }
}
