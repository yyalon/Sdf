using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sdf.Domain.Db
{
    public interface IDbContext : IDisposable
    {
        DbChangeResult SaveChage();
        IDbConnection GetDbConnection();
    }
}
