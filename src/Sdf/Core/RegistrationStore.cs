using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Core
{
    public class RegistrationStore
    {
        public bool IsCanClassInterceptors { get; set; }
        public Type TServer { get; set; }
        public Type TImp { get; set; }
        public string Name { get; set; }
        public object Registration { get; set; }
    }
}
