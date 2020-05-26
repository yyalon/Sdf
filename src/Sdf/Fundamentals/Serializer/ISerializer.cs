using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals.Serializer
{
    public interface ISerializer
    {
        string Serialize(object value);
        object Deserialize(string json);
        T Deserialize<T>(string json);
    }
}
