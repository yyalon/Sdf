using Sdf.Wechat.Work.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work.Service.Token.Msg
{
    public class AccessTokenMessage : BaseMessage
    {
        [System.Text.Json.Serialization.JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("expires_in")]
        public int ExpiresTime { get; set; }
      
    }
}
