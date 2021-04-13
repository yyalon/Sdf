using Microsoft.Extensions.DependencyInjection;
using Sdf.Core;
using Sdf.Wechat.Work.Service.OAuth;
using Sdf.Wechat.Work.Service.Token;
using Sdf.Wechat.Work.Service.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work
{
    public static class SdfConfigManagerExtensions
    {
        public static SdfConfigManager UseWorkWechat(this SdfConfigManager sdfConfig)
        {
            sdfConfig.Services.AddHttpClient(WechatUrlHelper.WECHAT_HTTP_CLIENT, configureClient => {
                configureClient.BaseAddress = new Uri("https://qyapi.weixin.qq.com");
            });
            sdfConfig.Register.RegisterTransient<IOAuthService, OAuthService>();
            sdfConfig.Register.RegisterSingleton<IAccessTokeStore, MemoryAccessTokeStore>();
            sdfConfig.Register.RegisterTransient<IAccessTokenManager, AccessTokenManager>();
            sdfConfig.Register.RegisterTransient<IOAuthService, OAuthService>();
            sdfConfig.Register.RegisterTransient<IUserService, UserService>();
            return sdfConfig;
        }
    }
}
