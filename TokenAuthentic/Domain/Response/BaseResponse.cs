using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenAuthentic.Domain.Response
{
    public class BaseResponse<T> where T: class
    {
        public T Extra { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public BaseResponse(T Extra=null)
        {
            this.Extra = Extra;
            this.Success = true;
        }
        public BaseResponse(string message)
        {
            this.Message = message;
            this.Success = false;
        }
    }
}
