using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work.Model
{
    public class BaseMessage
    {
        [System.Text.Json.Serialization.JsonPropertyName("errcode")]
        public int ErrCode { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("errmsg")]
        public string ErrMsg { get; set; }
    }
}
