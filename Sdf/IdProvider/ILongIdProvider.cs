using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.IdProvider
{
    public interface ILongIdProvider
    {
        long CreateNew();
    }
}
