using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work.Service.OAuth
{
    public interface IOAuthService
    {
        string GetOAuthConnect(string corpID, string redirect_uri, string state = null);
    }
}
