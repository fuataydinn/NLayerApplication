using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        //Bunu protected tanımladık, cunku custom repolarda olacak ilerde bunu da miras alıcaz.
        //protected erisim belirleyici, miras alan sınıf erisebilir.
        protected readonly AppDbContext _context; //db ile ilgili islem yapabilmek icin buna ihtiyac var 
        private readonly DbSet<T> _dbSet; //veri tabanındaki tabloya karsılık geliyor

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);  
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
           await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);   
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
          return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
           _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
          // _context.Entry(entities).State = EntityState.Deleted; Asagdaki ile bu metot aynı isi yapıyor 
          _dbSet.RemoveRange(entities); // burada silmiyor db'den sadece durumunu remove yapıyor.
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression); //Where zaten kendisi IQueryable donuyor
        }
    }
}
