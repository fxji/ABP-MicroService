using System;
using System.Collections.Generic;
using System.Text;

namespace AAA.A3Management.Dto
{
    public class ShareDto
    {
        public string EmailAddress { get; set; }
        
        public CreateOrUpdateA3Dto A3 { get; set; }
    }
}
