using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sdf.Fundamentals.Configuration
{
    public class DefaultConfig<T> : IConfig<T> where T : class
    {
        private readonly T _options;
        private IConfiguration _configuration;
        public DefaultConfig(IOptionsMonitor<T> optionsMonitor, IConfiguration configuration)
        {
            _options = optionsMonitor.CurrentValue;
            _configuration = configuration;
        }
       
        public T Instance => _options;

        public string GetValue(string key)
        {
            return _configuration[key];
        }

        public bool TryGetValue(string key, out string value)
        {
            var section= _configuration.GetSection(key);
            if (section != null)
            {
                value = section.Value;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}
