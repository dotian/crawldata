using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConGetData.Model
{
    [Flags]
    public enum SiteUseTypeEnum
    {
        None = 0,
        Common = 1,
        Post = 2,
        Search = 4,
        Paging = 8
    }
}
