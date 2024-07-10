using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventarios.Shared
{
    public class ResponseAPI<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseAPI() { }

        public ResponseAPI(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
