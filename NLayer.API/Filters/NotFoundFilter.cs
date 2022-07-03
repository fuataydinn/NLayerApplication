using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;
        
        //Eger bir filter bir service veya repoyu constructorda kullanıyorsa bunu startup tarafında belirtmek zorundayız !! 
        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //ProductController GetById metodunda id var mı yok mu kontrol edecek bu filter, daha Action metoduna varmadan ...

            var idValue = context.ActionArguments.Values.FirstOrDefault(); //id'yi burdan yakaladık, sonra kontrol edicez var mı ?

            if (idValue==null)
            {
                await next.Invoke();
                return;
            }
            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x=>x.Id==id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDTO<NoContentDTO>.Fail(404,$"{typeof(T).Name} ({id}) not found"));

            //Bunu aktif hale getirmemiz gerek 

        }
    }
}
