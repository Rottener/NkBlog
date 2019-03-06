using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NkBlog.IRepository;
using NkBolg.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NkBlog.Repository
{
    public class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : class
    {
        private readonly static Dictionary<DatabaseType, Type> dic = new Dictionary<DatabaseType, Type>
        {
            [DatabaseType.SqlServer] = typeof(SqlConnection),
            [DatabaseType.MySql] = typeof(MySqlConnection)
        };

        private readonly DapperOptions _options;
        public IDbConnection _dbConnection;


        public BaseRepository(IOptions<DapperOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options.Value;
            Type type = dic[_options.DatabaseType];
            _dbConnection = Activator.CreateInstance(type) as IDbConnection;
            _dbConnection.ConnectionString = _options.ConnectionString;
        }

        #region 同步

        public T Get(TKey id) => _dbConnection.Get<T>(id);
        public IEnumerable<T> GetList() => _dbConnection.GetList<T>();

        public IEnumerable<T> GetList(object whereConditions) => _dbConnection.GetList<T>(whereConditions);

        public IEnumerable<T> GetList(string conditions, object parameters = null) => _dbConnection.GetList<T>(conditions, parameters);

        public IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        {
            return _dbConnection.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby, parameters);
        }
        public int? Insert(T entity) => _dbConnection.Insert(entity);
        public int Update(T entity) => _dbConnection.Update(entity);

        public int Delete(TKey id) => _dbConnection.Delete<T>(id);

        public int Delete(T entity) => _dbConnection.Delete(entity);
        public int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConnection.DeleteList<T>(whereConditions, transaction, commandTimeout);
        }

        public int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConnection.DeleteList<T>(conditions, parameters, transaction, commandTimeout);
        }
        public int RecordCount(string conditions = "", object parameters = null)
        {
            return _dbConnection.RecordCount<T>(conditions, parameters);
        }
        #endregion

        #region 异步
        public async Task<T> GetAsync(TKey id)
        {
            return await _dbConnection.GetAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _dbConnection.GetListAsync<T>();
        }

        public async Task<IEnumerable<T>> GetListAsync(object whereConditions)
        {
            return await _dbConnection.GetListAsync<T>(whereConditions);
        }

        public async Task<IEnumerable<T>> GetListAsync(string conditions, object parameters = null)
        {
            return await _dbConnection.GetListAsync<T>(conditions, parameters);
        }
        public async Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        {
            return await _dbConnection.GetListPagedAsync<T>(pageNumber, rowsPerPage, conditions, orderby, parameters);
        }
        public async Task<int?> InsertAsync(T entity)
        {
            return await _dbConnection.InsertAsync(entity);
        }
        public async Task<int> UpdateAsync(T entity)
        {
            return await _dbConnection.UpdateAsync(entity);
        }
        public async Task<int> DeleteAsync(TKey id)
        {
            return await _dbConnection.DeleteAsync(id);
        }

        public async Task<int> DeleteAsync(T entity)
        {
            return await _dbConnection.DeleteAsync(entity);
        }


        public async Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await _dbConnection.DeleteListAsync<T>(whereConditions, transaction, commandTimeout);
        }

        public async Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await DeleteListAsync(conditions, parameters, transaction, commandTimeout);
        }
        public async Task<int> RecordCountAsync(string conditions = "", object parameters = null)
        {
            return await _dbConnection.RecordCountAsync<T>(conditions, parameters);
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~BaseRepository() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
