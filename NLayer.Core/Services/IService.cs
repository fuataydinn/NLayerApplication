using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {  
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities); 
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression); 

        //Servis katmanında degisiklikleri yansıtacagımız icin, asenkron oldu bu metotlar (SaveChange) calıstırılacak.
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
