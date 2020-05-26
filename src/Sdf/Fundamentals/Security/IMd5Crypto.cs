using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals.Security
{
    public interface IMd5Crypto
    {
        string Md5crypto32(string str);
        string ComputeMd5(byte[] bytes);
    }
}
