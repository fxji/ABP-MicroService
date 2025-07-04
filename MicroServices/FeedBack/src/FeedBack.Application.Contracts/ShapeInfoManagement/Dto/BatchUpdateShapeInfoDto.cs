using System;
using System.Collections.Generic;

public class BatchUpdateShapeInfoDto
{
    public List<Guid> Ids { get; set; }

    public string Cause { get; set; }
}