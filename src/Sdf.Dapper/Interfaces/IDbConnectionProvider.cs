using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sdf.Dapper.Interfaces
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetDbConnection();
    }
}
