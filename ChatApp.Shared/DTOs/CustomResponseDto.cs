using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatApp.Shared.DTOs
{
    public class CustomResponseDto
    {
        [JsonIgnore]
        public int StatusCode { get; set; }

        public List<string> ErrorMessage { get; set; }

        public CustomResponseDto Success(int statusCode)
        {
            return new CustomResponseDto() { StatusCode = statusCode, ErrorMessage = null };
        }

       
        public static CustomResponseDto Fail(int statusCode, List<string> errormessage)
        {
            return new CustomResponseDto() { StatusCode = statusCode, ErrorMessage = errormessage };
        }

        public static CustomResponseDto Fail(int statusCode, string errormessage)
        {
            return new CustomResponseDto() { StatusCode = statusCode, ErrorMessage= new List<string> { errormessage } };
        }
    }


    public class CustomResponseDto<T>
    {
        public T? Model { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        public List<string> ErrorMessage { get; set; }

        public CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode, ErrorMessage = null };
        }

        public CustomResponseDto<T> Success(int statusCode, T model)
        {
            return new CustomResponseDto<T>() { Model = model, StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errormessage)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode, ErrorMessage = errormessage };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string errormessage)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, ErrorMessage = new List<string> { errormessage } };
        }


    }
}
