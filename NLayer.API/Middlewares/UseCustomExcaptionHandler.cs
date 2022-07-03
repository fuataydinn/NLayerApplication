using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExcaptionHandler
    {
        //Bir extension metot yapmak icin static olmalı ve metot'ta static olmak zorunda ! 
        public static void UseCustomException(this IApplicationBuilder app)
        {
            //Bu interface icin bir extension metot olusturursak bunu implement eden tum claslarda kullanabiliriz.
            app.UseExceptionHandler(config =>
            {
                //Bu bir API oldugundan bir json donucez 

                config.Run(async context => 
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>(); //Hatayı verecek olan Interface'i implement ettik.

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _=> 500
                    };
                    context.Response.StatusCode = statusCode;

                    var response=CustomResponseDTO<NoContentDTO>.Fail(statusCode,exceptionFeature.Error.Message);

                    //Bu olusan bir tip, bunu response donmek icin Serilaze etmek zorundayız
                    //Controller'da bir tip oldugunda otomatik JSON doner ama burada ozel middleware yaptıgımız icin manuel JSON format yapmamız gerekiyor.

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                    //kendi middleware'imizi yazdık ama bunu aktif hale getirmek icin program cs tarafında belirt !!! 
                
                });


            });

        }
    }
}
