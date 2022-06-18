using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
  
    public class CustomBaseController : ControllerBase
    {
        //Bu controller ile geri deger donusunde bu kullanılacak hepsinde 

        [NonAction] //Bu bir endpoint degil bunu belirtmek icin koyduk bu attribute'u 
        public IActionResult CreateActionResult<T>(CustomResponseDTO<T> response)
        {
            if (response.StatusCode==204)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
            
        }
    }
}
