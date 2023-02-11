using RepositoryPatternWithUOW.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> expression);
        Task<T> GetOneByFilter(Expression<Func<T, bool>> expression);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entities);
        Task<T> Update(T entity);
        Task<T> Add(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        Task<IEnumerable<T>> FindAllIcluded(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllIcludedPagiantion(Expression<Func<T, bool>> criteria, int page, int pageSize, string[] includes = null);
        Task<IEnumerable<T>> FindAllIcludedPagiantion( int page, int pageSize, string[] includes = null);
        int GetCount();
        int GetCount(Expression<Func<T, bool>> expression);


    }
}