using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sdf.Modules
{
    public class ModuleHelper
    {
        public static bool IsModule(Type type, int layer)
        {
            if (layer == 4)
                return false;
            if (type == null)
                return false;
            if (type.BaseType == null)
                return false;
            if (type.BaseType.FullName == typeof(ModuleBase).FullName)
            {
                return true;
            }
            else if (type.BaseType.Equals(typeof(Object)))
            {
                return false;
            }
            else
            {
                return IsModule(type.BaseType.GetType(), layer + 1);
            }
        }
        public static Type AssemblyIsModule(Assembly assembly)
        {
            try
            {
                var types = assembly.GetTypes();
                foreach (var item in types)
                {
                    if (IsModule(item, 4))
                    {
                        return item;
                    }
                }
            }
            catch { }
            return null;
        }
    }
}
