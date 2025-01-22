using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sdf.Domain.Db
{
    public interface IDbContext : IDisposable
    {
        Task<DbChangeResult> SaveChageAsync(CancellationToken cancellationToken = default);
        IDbConnection GetDbConnection();
    }
}
