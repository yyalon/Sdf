using Sdf.Fundamentals.Logs;
using Sdf.Fundamentals.Serializer;
using Sdf.Wechat.Work.Model;
using Sdf.Wechat.Work.Service.Token.Msg;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Sdf.Wechat.Work.Service.Token
{
    public class AccessTokenManager : IAccessTokenManager
    {
        
        private readonly IAccessTokeStore _accessTokeStore;
        private readonly HttpClient _httpClient;
        private readonly ILog _logger;
        private readonly object lockObj = new object();
        private readonly ISerializer _serializer;
        public AccessTokenManager(IAccessTokeStore accessTokeStore,
            IHttpClientFactory clientFactory,
            ILog logger,
            ISerializer serializer)
        {
            _accessTokeStore = accessTokeStore;
            _httpClient = clientFactory.CreateClient(WechatUrlHelper.WECHAT_HTTP_CLIENT);
            _logger = logger;
            _serializer = serializer;
        }
        public AccessTokenModel GetAccessToken(string corpid, string corpsecret)
        {
            var accessToken = _accessTokeStore.GetAccessToken(corpid, corpsecret);
            if (accessToken == null || DateTime.Now >= accessToken.ExpiresTime)
            {
                lock (lockObj)
                {
                    try
                    {
                     
                        string url = WechatUrlHelper.GettokenUrl(corpid, corpsecret);
                        var res = _httpClient.SendGet<AccessTokenMessage>(url);
                        if (res.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var accessTokenMsg = res.Result;
                            if (accessTokenMsg.ErrCode == 0)
                            {
                                accessToken = AccessTokenMsgMap(accessTokenMsg);
                                var tmpAccessToken = _accessTokeStore.GetAccessToken(corpid, corpsecret);
                                if (tmpAccessToken == null || DateTime.Now >= tmpAccessToken.ExpiresTime)
                                {
                                    _accessTokeStore.SaveAccessToken(corpid, corpsecret, accessToken);
                                    return accessToken;
                                }
                                return tmpAccessToken;
                            }
                            else
                            {
                                _logger.LogError($"获取accessToken失败,错误码:{accessTokenMsg.ErrCode}，错误消息:{accessTokenMsg.ErrMsg}，corpid：{corpid}，corpsecret：{corpsecret}");
                                return null;
                            }
                        }
                        else
                        {
                            _logger.LogError($"请求accessToken失败,http错误码:{res.HttpStatusCode},Url:{url}");
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"获取accessToken发生异常,{ex.Message}", ex);
                        return null;
                    }
                }
            }
            else
            {
                return accessToken;
            }
        }
        private AccessTokenModel AccessTokenMsgMap(AccessTokenMessage accessTokenMessage)
        {
            return new AccessTokenModel(accessTokenMessage.AccessToken, DateTime.Now.ToUniversalTime().AddSeconds(accessTokenMessage.ExpiresTime));
        }
        //private AccessTokenModel AccessTokenWxMsgMap(AccessTokenMessage accessTokenMessage)
        //{
        //    return new AccessTokenModel(accessTokenMessage.OpenId, accessTokenMessage.AccessToken, DateTime.Now.ToUniversalTime().AddSeconds(accessTokenMessage.ExpiresIn));
        //}
        //public AccessTokenModel GetAccessToken(string appid, string appsecrest, string code)
        //{
        //    var accessToken = _accessTokeStore.GetAccessToken(appid, appsecrest);
        //    if (accessToken == null)
        //    {
        //        lock (lockObj)
        //        {
        //            accessToken = _accessTokeStore.GetAccessToken(appid, appsecrest);
        //            if (accessToken == null)
        //            {
        //                try
        //                {
                        
        //                    string url = WechatUrlHelper.GetWxtokenUrl(appid, appsecrest, code);
        //                    var res = _httpClient.SendGet<AccessTokenMessage>(url);
        //                    if (res.HttpStatusCode == System.Net.HttpStatusCode.OK)
        //                    {
        //                        var accessTokenMsg = res.Result;
        //                        if (accessTokenMsg.ErrCode == 0)
        //                        {
        //                            accessToken = AccessTokenWxMsgMap(accessTokenMsg);
        //                            _accessTokeStore.SaveAccessToken(appid, appsecrest, accessToken);
        //                        }
        //                        else
        //                        {
        //                            _logger.LogError($"获取accessToken失败,错误码:{accessTokenMsg.ErrCode}，错误消息:{accessTokenMsg.ErrMsg}，corpid：{appid}，corpsecret：{appsecrest}");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        _logger.LogError($"请求accessToken失败,http错误码:{res.HttpStatusCode},Url:{url}");
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    _logger.LogError($"获取accessToken发生异常,{ex.Message}", ex);
        //                }
        //            }
        //            return accessToken;
        //        }
        //    }
        //    else
        //    {
        //        return accessToken;
        //    }
        //}

    }
}
