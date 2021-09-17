using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals
{
    public interface IDateTimeProvider
    {
        DateTime GetNow();
    }
}
