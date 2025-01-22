using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Domain.Uow
{
    public class UowAttribute : Attribute
    {
        private bool enabled = true;
        public UowAttribute()
        {
            enabled = true;
        }
        public UowAttribute(bool enabled)
        {
            this.enabled = enabled;
        }
        public bool Enabled
        {
            get
            {
                return enabled;
            }
        }
    }
}
