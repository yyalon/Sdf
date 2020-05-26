using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.IdProvider.Default
{
    public class DefaultLongIdProvider : ILongIdProvider
    {
        public long CreateNew()
        {
            return IdGenerator.Instance.GetLongId();
        }
    }
}
