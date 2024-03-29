using System.Threading;
using System.Threading.Tasks;

namespace Sdf.Fundamentals.Security
{
    public interface IMd5Crypto
    {
        Task<string> Md5crypto32Async(string str, CancellationToken cancellationToken);

        Task<string> ComputeMd5Async(byte[] bytes, CancellationToken cancellationToken);
    }
}
