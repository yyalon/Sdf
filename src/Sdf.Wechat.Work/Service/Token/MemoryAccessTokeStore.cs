using Sdf.Wechat.Work.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sdf.Wechat.Work.Service.Token
{
    public class MemoryAccessTokeStore : IAccessTokeStore
    {
        private static Dictionary<string, AccessTokenModel> dic = new Dictionary<string, AccessTokenModel>();
        public AccessTokenModel GetAccessToken(string corpid, string corpsecret)
        {
            AccessTokenModel accessToken = null;
            dic.TryGetValue($"{corpid}@{corpsecret}", out accessToken);
            return accessToken;
        }

        public void SaveAccessToken(string corpid, string corpsecret, AccessTokenModel accessToken)
        {
            string key = $"{corpid}@{corpsecret}";
            if (dic.Keys.Any(m => m == key))
            {
                dic[key] = accessToken;
            }
            else
            {
                dic.Add(key, accessToken);
            }
        }
    }
}
