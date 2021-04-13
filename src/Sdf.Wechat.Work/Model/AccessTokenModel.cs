using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work.Model
{
    [Serializable]
    public class AccessTokenModel
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresTime { get; set; }

      
        public AccessTokenModel()
        {
        }

        public AccessTokenModel(string accessToken, DateTime expiresTime)
        {
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            ExpiresTime = expiresTime;
        }
    }
}
