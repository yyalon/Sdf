using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sdf.Fundamentals.Security
{
    public class DefaultMd5Crypto : IMd5Crypto
    {
        public async Task<string> Md5crypto32Async(string str, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            return await ComputeMd5Async(Encoding.Default.GetBytes(str), cancellationToken);
        }

        public async Task<string> ComputeMd5Async(byte[]  bytes, CancellationToken cancellationToken)
        {
            using var md5Hasher = MD5.Create();
            using var stream=new MemoryStream(bytes);
            byte[] data =await md5Hasher.ComputeHashAsync(stream, cancellationToken);
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
