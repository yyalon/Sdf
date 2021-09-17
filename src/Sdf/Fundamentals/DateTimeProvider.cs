using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals
{
    public class DateTimeProvider : IDateTimeProvider
    {
        private bool _isUtc = false;
        public DateTimeProvider(bool isUtc=false)
        {
            _isUtc = isUtc;
        }
        public DateTime GetNow()
        {
            if (_isUtc)
            {
                return DateTime.UtcNow;
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}
