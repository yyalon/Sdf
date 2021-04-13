using Sdf.Wechat.Work.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work.Service.Token
{
    public interface IAccessTokenManager
    {
        AccessTokenModel GetAccessToken(string appid, string appsecrest);
    }
}
