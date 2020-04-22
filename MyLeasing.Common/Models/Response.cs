using System;
using System.Collections.Generic;
using System.Text;

namespace MyLeasing.Common.Models
{
    public class Response<T> where T : class //devuelve si se pudo utilizar el API o no (y por que)
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }
    }

}
