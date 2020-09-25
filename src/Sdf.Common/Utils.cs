using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sdf.Common
{
    public class Utils
    {
        public static string Combine(params string[] paths)
        {
            string path = Path.Combine(paths);
            return path.Replace("\\", "/");
        }
    }
}
