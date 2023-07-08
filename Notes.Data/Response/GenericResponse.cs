using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Response
{
    public class GenericResponse<T>
    {
        public bool Success { get; private set; }
        public List<string> Message { get; private set; }
        public T Response { get; private set; }

        public GenericResponse(bool isSucces)
        {
            Response = default;
            Success = isSucces;
            Message = isSucces ? new List<string>() { "Success" } : new List<string>() { "Fault" } ;
        }

        public GenericResponse(T resource)
        {
            Success = true;
            Message = new List<string>() { "Success" };
            Response = resource; ;
        }

        public GenericResponse(string message)
        {
            Success = false;
            Response = default;

            if (!string.IsNullOrEmpty(message))
            {
                Message = new List<string>() { message };
            }
        }

        public GenericResponse(List<string> messages)
        {
            this.Success = false;
            this.Response = default;
            this.Message = messages ?? new List<string>() { "Fault" };
        }
    }
}
