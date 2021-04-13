using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Wechat.Work.Model
{
    public class WechatUserInfo
    {
        public WechatUserInfo()
        { 
        
        }
        public WechatUserInfo(string openId, string deviceId)
        {
            OpenId = openId ?? throw new ArgumentNullException(nameof(openId));
            DeviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
        }

        public string OpenId { get; set; }
        public string DeviceId { get; set; }
    }
}
