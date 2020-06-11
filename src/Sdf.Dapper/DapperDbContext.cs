using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Sdf.Common;
using Sdf.Dapper.Interfaces;
using Sdf.Domain.Db;

namespace Sdf.Dapper
{
    public class DapperDbContext : IDbContext
    {
       
        private readonly IDbChangeEventHandler _dbChangeEventHandler;
        private readonly DapperOption _dapperOption;
        private readonly ILogger _logger;
        private readonly IDbConnectionProvider _dbConnectionProvider;
        public DapperDbContext(IDbChangeEventHandler dbChangeEventHandler
            , DapperOption dapperOption
            , ILogger<DapperDbContext> logger
            , IDbConnectionProvider dbConnectionProvider)
        {
            _dbChangeEventHandler = dbChangeEventHandler;
            _dapperOption = dapperOption;
            _logger = logger;
            _dbConnectionProvider = dbConnectionProvider;
        }
        private List<Exception> Exceptions = new List<Exception>();
        private IDbConnection dbConnection;

        public IDbTransaction Transaction { get; private set; }
        
        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
            if (dbConnection != null)
            {
                
                dbConnection.Dispose();
                dbConnection = null;
                
            }
            
        }

        public IDbConnection GetDbConnection()
        {
            if (dbConnection == null)
            {
                switch (_dapperOption.DbType)
                {
                    case DbType.MSSQL:
                        dbConnection = _dbConnectionProvider.GetDbConnection();
                        break;
                    case DbType.MYSQL:
                        throw new Exception("mysql 暂时不支持");
                    default:
                        break;
                }
            }
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }
        public IDbTransaction BeginTransaction()
        {
            if (Transaction == null)
            {
                Transaction = GetDbConnection().BeginTransaction();
            }
            return Transaction;
        }
        

        public IEnumerable<T> GetList<T>(string sql, object param = null) 
        {
            return GetDbConnection().Query<T>(sql, param, BeginTransaction(), true, _dapperOption.CommontTimeOut);

        }
        public T SelectFirse<T>(string sql, object param = null)
        {
            return GetDbConnection().QuerySingleOrDefault<T>(sql, param, BeginTransaction(), _dapperOption.CommontTimeOut);

        }
        public T ExecuteScalar<T>(string sql, object param = null)
        {
            return GetDbConnection().ExecuteScalar<T>(sql, param, BeginTransaction(), _dapperOption.CommontTimeOut);
        }
        public object ExecuteScalar(string sql, object param = null)
        {
            return GetDbConnection().ExecuteScalar(sql, param, BeginTransaction(), _dapperOption.CommontTimeOut);
        }

        private int dbChangeNumber = 0;
        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                GetDbConnection().Insert(entity, BeginTransaction(), _dapperOption.CommontTimeOut);
                dbChangeNumber++;
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }

        }
        public void InsertBulk<TEntity>(IEnumerable<TEntity> list) where TEntity : class
        {
            try
            {
                foreach (var item in list)
                {
                    GetDbConnection().Insert(item, BeginTransaction(), _dapperOption.CommontTimeOut);
                    dbChangeNumber++;
                }
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }
  
        }
        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                GetDbConnection().Update<TEntity>(entity, BeginTransaction(), _dapperOption.CommontTimeOut);
                dbChangeNumber++;
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }

        }
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                GetDbConnection().Delete(entity, BeginTransaction(), _dapperOption.CommontTimeOut);
                dbChangeNumber++;
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
            }

        }
       
        public int Execute(string sql, object param = null)
        {
            try
            {
                int res = GetDbConnection().Execute(sql, param, BeginTransaction(), _dapperOption.CommontTimeOut);
                dbChangeNumber += res;
                return res;
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
                return 0;
            }

        }

        public DbChangeResult SaveChage()
        {

            if (Transaction != null)
            {
                if (Exceptions.Count > 0)
                {
                    Transaction.Rollback();
                    return new DbChangeResult(0, false, Exceptions);
                }
                try
                {
                    Transaction.Commit();
                    return new DbChangeResult(dbChangeNumber, true);
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    var innerException = ExceptionHelper.GetInnerExceptionMess(ex);
                    _logger.LogWarning(innerException, innerException.Message);
                    return new DbChangeResult(0, false, new List<Exception>() { innerException });
                }

            }
            else 
            {
                return new DbChangeResult(dbChangeNumber, true);
            }
        }
    }
}
