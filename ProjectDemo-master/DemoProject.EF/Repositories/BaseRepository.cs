using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryPatternWithUOW.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public int GetCount()
        {
            return  _context.Set<T>().Count();
        }

        public int GetCount(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).Count();
        }
        public async Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetOneByFilter(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> FindAllIcluded(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(criteria).ToList();
        }
        public async Task<IEnumerable<T>> FindAllIcludedPagiantion(Expression<Func<T, bool>> criteria,int page,int pageSize,string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(criteria).Skip((page-1)*pageSize).Take(pageSize).ToList();
        }
        public async Task<IEnumerable<T>> FindAllIcludedPagiantion( int page, int pageSize, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}