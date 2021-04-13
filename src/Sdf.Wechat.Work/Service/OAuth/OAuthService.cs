using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work.Service.OAuth
{
    public class OAuthService : IOAuthService
    {
        public string GetOAuthConnect(string corpID, string redirect_uri, string state = null)
        {
            return WechatUrlHelper.OAuthConnect(corpID, redirect_uri, state);
        }
    }
}
