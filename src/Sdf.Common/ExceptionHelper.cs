using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Common
{
    public class ExceptionHelper
    {
        public static Exception GetInnerExceptionMess(Exception ex)
        {
            if (ex.InnerException == null)
                return ex;
            else
                return GetInnerExceptionMess(ex.InnerException);
        }
    }
}
