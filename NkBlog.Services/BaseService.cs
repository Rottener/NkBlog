using NkBlog.IRepository;
using NkBlog.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace NkBlog.Services
{
    public class BaseService<T, Tkey> : IBaseService<T, Tkey> where T : class
    {
        private readonly IBaseRepository<T, Tkey> _baseRepository;

        public BaseService(IBaseRepository<T, Tkey> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public int Delete(Tkey id)
        {
            return _baseRepository.Delete(id);
        }

        public int Delete(T entity)
        {
            return _baseRepository.Delete(entity);
        }

        public Task<int> DeleteAsync(Tkey id)
        {
            return _baseRepository.DeleteAsync(id);
        }

        public Task<int> DeleteAsync(T entity)
        {
            return _baseRepository.DeleteAsync(entity);
        }

        public int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _baseRepository.DeleteList(whereConditions, transaction = null, commandTimeout = null);
        }

        public int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _baseRepository.DeleteList(conditions, parameters = null, transaction = null, commandTimeout = null);
        }

        public Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _baseRepository.DeleteListAsync(whereConditions, transaction = null, commandTimeout = null);
        }

        public Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _baseRepository.DeleteListAsync(conditions, parameters = null, transaction = null, commandTimeout = null);
        }

        public T Get(Tkey id)
        {
            return _baseRepository.Get(id);
        }

        public Task<T> GetAsync(Tkey id)
        {
            return _baseRepository.GetAsync(id);
        }

        public IEnumerable<T> GetList()
        {
            return _baseRepository.GetList();
        }

        public IEnumerable<T> GetList(object whereConditions)
        {
            return _baseRepository.GetList(whereConditions);
        }

        public IEnumerable<T> GetList(string conditions, object parameters = null)
        {
            return _baseRepository.GetList(conditions, parameters = null);
        }

        public Task<IEnumerable<T>> GetListAsync()
        {
            return _baseRepository.GetListAsync();
        }

        public Task<IEnumerable<T>> GetListAsync(object whereConditions)
        {
            return _baseRepository.GetListAsync(whereConditions);
        }

        public Task<IEnumerable<T>> GetListAsync(string conditions, object parameters = null)
        {
            return _baseRepository.GetListAsync(conditions, parameters = null);
        }

        public IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        {
            return _baseRepository.GetListPaged(pageNumber, rowsPerPage, conditions, orderby, parameters = null);
        }

        public Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        {
            return _baseRepository.GetListPagedAsync(pageNumber, rowsPerPage, conditions, orderby, parameters = null);
        }

        public int? Insert(T entity)
        {
            return _baseRepository.Insert(entity);
        }

        public Task<int?> InsertAsync(T entity)
        {
            return _baseRepository.InsertAsync(entity);
        }

        public int RecordCount(string conditions = "", object parameters = null)
        {
            return _baseRepository.RecordCount(conditions = "", parameters = null);
        }

        public Task<int> RecordCountAsync(string conditions = "", object parameters = null)
        {
            return _baseRepository.RecordCountAsync(conditions = "", parameters = null);
        }

        public int Update(T entity)
        {
            return _baseRepository.Update(entity);
        }

        public Task<int> UpdateAsync(T entity)
        {
            return _baseRepository.UpdateAsync(entity);
        }
    }
}
