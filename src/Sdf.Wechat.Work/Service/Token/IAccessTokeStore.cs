using Sdf.Wechat.Work.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work.Service.Token
{
    public interface IAccessTokeStore
    {
        AccessTokenModel GetAccessToken(string corpid, string corpsecret);
        void SaveAccessToken(string corpid, string corpsecret, AccessTokenModel accessToken);
    }
}
