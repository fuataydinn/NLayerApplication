using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDTO<T>
    {
        //API'ye ozgu olan bir response yazıyoruz burada. Her istek icin tek tek response sınıfı donmek yerine merkezi bir sınıf olusturduk
        //burada, buradan basarılı veya basarısız donucez.
        public T Data { get; set; }
        public List<String> Errors { get; set; }

        [JsonIgnore] //bunu json'a donustururken .. API'ye verme.
        public int StatusCode { get; set; }


        //Herhangi bir class'ın icerisinde static ve new ile yeni bir instance donen metot varsa bunlar STATİC FACTOR metot denir
        public static CustomResponseDTO<T> Success(int statusCode, T data)
        {
            return new CustomResponseDTO<T> { Data = data, StatusCode = statusCode };
        }

        public static CustomResponseDTO<T> Success(int statusCode)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode };
        }

        public static CustomResponseDTO<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode, Errors = errors };
        }

        public static CustomResponseDTO<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }

    }
}
