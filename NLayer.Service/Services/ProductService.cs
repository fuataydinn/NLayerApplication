using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            //Diger yukarıdakiler kendi sınıflarında private old icin kullanılmıyor zaten
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategory()
        {
            var product = await _productRepository.GetProductsWithCategory();
            //burada direk CustomResponseDTO turunde veri donucez API'nin bizden bekledigi tur olan yani

            var productsDTO= _mapper.Map<List<ProductWithCategoryDTO>>(product);

            return CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(200, productsDTO);


        }
    }
}
