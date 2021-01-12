using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Sdf.Common
{
    public class Utils
    {
        public static string Combine(params string[] paths)
        {
            if (paths.Length >= 2)
            {
                if (paths[1].FirstOrDefault() == '/' || paths[1].FirstOrDefault() == '\\')
                {
                    paths[1] = paths[1].Substring(1);
                }
            }
            string path = Path.Combine(paths);
            return path.Replace("\\", "/");
        }
    }
}
