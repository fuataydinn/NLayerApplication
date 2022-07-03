using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribure :ActionFilterAttribute
    {
        //Bir Action metoda girmeden once, girikten sonra , girip sonuc vermeden onceki gibi asamalarda kontrolu elımıze almamıza olanak saglar filter'lar.

        public override void OnActionExecuting(ActionExecutingContext context) //override yazınca bu metot cıkıyor 
        {
            if (!context.ModelState.IsValid) //bir hata var ise 
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDTO<NoContentDTO>.Fail(400,errors));

                //Bu sınıfı tanıtmak icin programa tek tek Controllerlara yazmak yerine , program.cs dosyasının içerisine git ve orada global olarak tanımla.
             
                //Builder.Services.AddController( ) ; bunun parantezinin icine yazılır.
            }
        }


    }
}
