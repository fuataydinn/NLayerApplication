using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductService :IService<Product>
    {
        //Bu service icin ozellestirilmis DTO yaptık, hem product hem de category iceren
        //Servisler istenilen tam özellikteki DTO'yu doner 

        //Artık burada direk CustomResponseDTO donucez, Apı tarafında kod karmaşası olmasın diye 
        Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategory();
    }
}
