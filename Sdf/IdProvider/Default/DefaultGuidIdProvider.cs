using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.IdProvider.Default
{
    public class DefaultGuidIdProvider : IGuidIdProvider
    {
        public Guid CreateNew()
        {
            return Guid.NewGuid();
        }
    }
}
