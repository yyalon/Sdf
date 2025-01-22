using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sdf.Fundamentals.Serializer
{
    public interface ISerializer
    {
        Task<string> SerializeAsync(object value);
        Task<object> DeserializeAsync(string json);
        Task<T> DeserializeAsync<T>(string json);
    }
}
