using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work
{
    public class WechatUrlHelper
    {
        public const string WECHAT_HTTP_CLIENT = "wechat";
        private const string _qrConnect = "https://open.work.weixin.qq.com/wwopen/sso/qrConnect?appid={0}&agentid={1}&redirect_uri={2}&state={2}";
        private const string _gettoken = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}";
        private const string _getuserinfo = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}";
        private const string _oauthConnect = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect";
        private const string _getwxtoken = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        private const string _createUser = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}";
        private const string _updateUser = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0}";
        private const string _deleteUser = "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}";
        private const string _batchdeleteUser = "https://qyapi.weixin.qq.com/cgi-bin/user/batchdelete?access_token={0}";
        private const string _convert_to_openid = "https://qyapi.weixin.qq.com/cgi-bin/user/convert_to_openid?access_token={0}";
        private const string _convert_to_userid = "https://qyapi.weixin.qq.com/cgi-bin/user/convert_to_userid?access_token={0}";
        private const string _createDepartment = "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}";
        private const string _updateDepartment = "https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0}";
        private const string _deleteDepartment = "https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token={0}&id={1}";
        private const string _getDepartments = "https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}&id={1}";
        private const string _inviteUser = "https://qyapi.weixin.qq.com/cgi-bin/batch/invite?access_token={0}";
        private const string _readUser = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}";
        /// <summary>
        /// 请求企业微信登录二维码地址
        /// </summary>
        /// <param name="corpid">企业微信的CorpID，在企业微信管理端查看</param>
        /// <param name="agentid">授权方的网页应用ID，在具体的网页应用中查看</param>
        /// <param name="redirect_uri">	重定向地址，需要进行UrlEncode</param>
        /// <param name="state">用于保持请求和回调的状态，授权请求后原样带回给企业。该参数可用于防止csrf攻击（跨站请求伪造攻击），建议企业带上该参数，可设置为简单的随机数加session进行校验</param>
        /// <returns></returns>
        public static string QrConnect(string corpid, string agentid, string redirect_uri, string state)
        {
            return string.Format(_qrConnect, corpid, agentid, redirect_uri, state);
        }
        /// <summary>
        /// 构造网页授权链接
        /// </summary>
        /// <param name="corpID">企业的CorpID</param>
        /// <param name="redirect_uri">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        /// <param name="scope">应用授权作用域。企业自建应用固定填写：snsapi_base</param>
        /// <param name="state">重定向后会带上state参数，企业可以填写a-zA-Z0-9的参数值，长度不可超过128个字节</param>
        /// <returns></returns>
        public static string OAuthConnect(string corpID, string redirect_uri, string state = null)
        {
            return string.Format(_oauthConnect, corpID, redirect_uri, state);
        }
        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <param name="corpid">企业ID</param>
        /// <param name="corpsecret">应用的凭证密钥</param>
        /// <returns></returns>
        public static string GettokenUrl(string corpid, string corpsecret)
        {
            return string.Format(_gettoken, corpid, corpsecret);
        }
        public static string GetWxtokenUrl(string appid, string appsecret, string code)
        {
            return string.Format(_getwxtoken, appid, appsecret, code);
        }
        /// <summary>
        /// 通过code获取用户信息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="code">通过成员授权获取到的code，每次成员授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期</param>
        /// <returns></returns>
        public static string GetuserinfoUrl(string access_token, string code)
        {
            return string.Format(_getuserinfo, access_token, code);
        }
        /// <summary>
        /// 创建成员
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string CreateUserUrl(string access_token)
        {
            return string.Format(_createUser, access_token);
        }
        /// <summary>
        /// userid转openid
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string Convert2OpenidUrl(string access_token)
        {
            return string.Format(_convert_to_openid, access_token);
        }
        /// <summary>
        /// openid转userid
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string Convert2UseridUrl(string access_token)
        {
            return string.Format(_convert_to_userid, access_token);
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="access_toke"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string DeleteDepartmentUrl(string access_toke, int id)
        {
            return string.Format(_deleteDepartment, access_toke, id);
        }
        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string UpdateDepartmentUrl(string access_token)
        {
            return string.Format(_updateDepartment, access_token);
        }
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string CreateDepartmentUrl(string access_token)
        {
            return string.Format(_createDepartment, access_token);
        }
        /// <summary>
        /// 更新成员
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string UpdateUserUrl(string access_token)
        {
            return string.Format(_updateUser, access_token);
        }
        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="access_toke"></param>
        /// <returns></returns>
        public static string DeleteUserUrl(string access_toke, string userId)
        {
            return string.Format(_deleteUser, access_toke, userId);
        }
        /// <summary>
        /// 批量删除成员
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string BatchDeleteUserUrl(string access_token)
        {
            return string.Format(_batchdeleteUser, access_token);
        }
        /// <summary>
        /// 邀请成员
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string InviteUserUrl(string access_token)
        {
            return string.Format(_inviteUser, access_token);
        }
        /// <summary>
        /// 读取成员
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string ReadUserUrl(string access_token, string userId)
        {
            return string.Format(_readUser, access_token, userId);
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="rootId"></param>
        /// <returns></returns>
        public static string GetDepartments(string access_token, int rootId = 1)
        {
            return string.Format(_getDepartments, access_token, rootId);
        }
    }
}
