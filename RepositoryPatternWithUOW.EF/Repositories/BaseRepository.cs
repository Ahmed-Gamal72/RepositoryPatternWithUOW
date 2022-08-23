using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<T> GetAll()
        {
           return _context.Set<T>().ToList();   
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes !=null)
            {
                foreach (var include in includes)
                    query=query.Include(include);
            }
            return query.SingleOrDefault(match);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return query.Where(match).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match,int take , int skip)
        {
           
            return _context.Set<T>().Where(match).Skip(skip).Take(take).ToList();
        }
    }
}
