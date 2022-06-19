using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Mapping
{
    public class MapProfile : Profile
    {
        //Mapleme islemini burada gerceklestiricez
        
      public MapProfile() 
        { 
        CreateMap<Product,ProductDTO>().ReverseMap();
        CreateMap<Category,CategoryDTO>().ReverseMap();
        CreateMap<ProductFeature,ProductFeature>().ReverseMap();
        CreateMap<ProductUpdateDTO,Product>(); // bunu sadece product'a cevirmek icin kullanıcaz. Tersine gerek olmadıgı icin Reverse yok.
        CreateMap<Product,ProductWithCategoryDTO>(); // 33. Video 26:22
        CreateMap<Category, CategoryWithProductsDTO>();
        }


    }
}
