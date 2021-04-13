using Sdf.Wechat.Work.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work.Service.User.Msg
{
    public class UserInfoMessage: BaseMessage
    {
        [System.Text.Json.Serialization.JsonPropertyName("UserId")]
        public string UserId { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("OpenId")]
        public string OpenId { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("DeviceId")]
        public string DeviceId { get; set; }
    }
}
