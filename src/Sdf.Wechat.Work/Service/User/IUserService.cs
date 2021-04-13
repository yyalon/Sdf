using Sdf.Application;
using Sdf.Wechat.Work.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work.Service.User
{
    public interface IUserService
    {
        OperationResult<WechatUserInfo> GetWechatUserInfo(string access_token, string code);
    }
}
