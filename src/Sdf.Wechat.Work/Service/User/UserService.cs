using Sdf.Application;
using Sdf.Fundamentals.Logs;
using Sdf.Fundamentals.Serializer;
using Sdf.Wechat.Work.Model;
using Sdf.Wechat.Work.Service.User.Msg;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Sdf.Wechat.Work.Service.User
{
    public class UserService : IUserService
    {
        
        private readonly HttpClient _httpClient;
        private readonly ILog _logger;
        private readonly ISerializer _serializer;
        public UserService(IHttpClientFactory clientFactory,
            ILog logger,
            ISerializer serializer)
        {
            _httpClient = clientFactory.CreateClient(WechatUrlHelper.WECHAT_HTTP_CLIENT);
            _logger = logger;
            _serializer = serializer;
        }
        public OperationResult<WechatUserInfo> GetWechatUserInfo(string access_token, string code)
        {
            string url = WechatUrlHelper.GetuserinfoUrl(access_token, code);
            var res = _httpClient.SendGet<UserInfoMessage>(url);
            if (res.HttpStatusCode == System.Net.HttpStatusCode.OK && res.Result != null)
            {
                var msg = res.Result;
                if (msg.ErrCode == 0)
                {
                    string openId = string.IsNullOrEmpty(msg.UserId) ? msg.OpenId : msg.UserId;
                    return OperationResult<WechatUserInfo>.CreateSuccessResult(new WechatUserInfo(openId, msg.DeviceId));
                }
                else
                {
                    return OperationResult<WechatUserInfo>.CreateFailedResult($"ErrCode:{msg.ErrCode} ErrMsg:{msg.ErrMsg}");
                }
               
            }
            else
            {
                return OperationResult<WechatUserInfo>.CreateFailedResult($"http error code {res.HttpStatusCode}");
            }
        }
    }
}
