using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        //productRepository.GetAll(x=>x.id>5).ToList();
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        //Burada direk veri tabanına sorgu yapmadıgımız icin asenkron degil

        //productRepository.where(x=>x.id>5).ToListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); 
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities); //Soyut nesnelerle calısmaya calıs genericlerde
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression); //Var mı ? Yok mu ? 
        void Update(T entity); //Update ve remove asenkron metod degıl, bunlarda ef'de sadece state'i degistigi icin 
        //Database'i yoran islemler degildir.
        void Remove(T entity);   
        void RemoveRange(IEnumerable<T> entities);

    }
}
