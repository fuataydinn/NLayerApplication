using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IService<Product> service, IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _service = productService;
        }

        // api/products/ GetProductsWithCategory   -------------custom ActionResult
        [HttpGet("GetProductsWithCategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _service.GetProductsWithCategory());
            //Maplame islemini burada yapmadıgımız icin tek satıra indi bu kod..
            //Yeni olusturdugun DTO yu mapper eklemeyi unutma
        }

        // api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            var productDTOs = _mapper.Map<List<ProductDTO>>(products.ToList());

            return CreateActionResult(CustomResponseDTO<List<ProductDTO>>.Success(200, productDTOs));
        }

        // /api/prodcut/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDTO = _mapper.Map<ProductDTO>(product);

            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(200, productDTO));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDTO)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDTO));  //product cevirdik
            var productsDTO = _mapper.Map<ProductDTO>(product); //productDTO cevirdik

            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(201, productsDTO));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO productDTO)
        {
            //Geri birsey donmuyor ve maplemeye de gerek yok.. Update de 
            await _service.UpdateAsync(_mapper.Map<Product>(productDTO));


            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204)); //data donmedigimiz icin NoContent
        }

        //api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);


            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }
    }
}
