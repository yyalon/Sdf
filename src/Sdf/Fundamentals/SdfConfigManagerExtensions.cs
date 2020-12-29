using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdf.Core;
using Sdf.Fundamentals.Configuration;
using Sdf.Fundamentals.Json;
using Sdf.Fundamentals.Security;
using Sdf.Fundamentals.Serializer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Sdf.Fundamentals
{
    public static class SdfConfigManagerExtensions
    {
        public static SdfConfigManager UseTextJsonSerializer(this SdfConfigManager sdfConfig, Action<JsonSerializerOptions> action)
        {
            TextJsonSerializer textJsonSerializer = new TextJsonSerializer();
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.Converters.Add(new JsonNonStringKeyDictionaryConverterFactory());
            jsonSerializerOptions.Converters.Add(new LongJsonConverter());
            jsonSerializerOptions.Converters.Add(new LongAvailableNullJsonConverter());
            jsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
            jsonSerializerOptions.Converters.Add(new DatetimeAvailableNullJsonConverter());
            jsonSerializerOptions.Converters.Add(new DoubleJsonConverter());
            jsonSerializerOptions.Converters.Add(new DoubleAvailableNullJsonConverter());
            if (action != null)
                action(jsonSerializerOptions);
            textJsonSerializer.JsonSerializerOptions = jsonSerializerOptions;
            sdfConfig.Register.RegisterSingleton<ISerializer>(textJsonSerializer);
            return sdfConfig;
        }
        public static SdfConfigManager UseTripleDES(this SdfConfigManager sdfConfig,string cryptoKey=null)
        {
            TripleDES tripleDES = new TripleDES();
            if (string.IsNullOrEmpty(cryptoKey))
            {
                cryptoKey = "sblw-3hn8-bvoy19";
            }
            tripleDES.DefaultCryptoKey = cryptoKey;
            sdfConfig.Register.RegisterSingleton<IDESProvider>(tripleDES);
            return sdfConfig;
        }
        public static SdfConfigManager UseTripleDES(this SdfConfigManager sdfConfig, Func<IResolver,string> action)
        {   
            sdfConfig.Register.RegisterSingleton<IDESProvider>(resolver=> {
                string cryptoKey = action(resolver);
                TripleDES tripleDES = new TripleDES();
                tripleDES.DefaultCryptoKey = cryptoKey;
                return tripleDES;
            });
            return sdfConfig;
        }
        public static SdfConfigManager UseDefaultMd5Crypto(this SdfConfigManager sdfConfig)
        {
            sdfConfig.Register.RegisterSingleton<IMd5Crypto, DefaultMd5Crypto>();
            return sdfConfig;
        }
        public static SdfConfigManager UseDefaultConfig<T>(this SdfConfigManager sdfConfig, IConfiguration configuration) where T:class
        {
            if (sdfConfig.Services == null)
            {
                throw new Exception("先传入Services");
            }
            sdfConfig.Services.Configure<T>(configuration);
            sdfConfig.Register.RegisterSingleton<IConfiguration>(configuration);
            sdfConfig.Register.RegisterSingleton<IConfig<T>, DefaultConfig<T>>();
            return sdfConfig;
        }
        public static SdfConfigManager UseDefaultFundamentals<TConfig>(this SdfConfigManager sdfConfig
            , IConfiguration configuration
            , Action<JsonSerializerOptions> action = null
            , string cryptoKey = null ) where TConfig : class
        {
            return sdfConfig.UseDefaultConfig<TConfig>(configuration).UseTextJsonSerializer(action).UseTripleDES(cryptoKey).UseDefaultMd5Crypto();
        }
    }
}
