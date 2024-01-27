using BookShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
    public interface IBaseResponse<T>
    {
        T Data { get; set; }
        StatusCode StatusCode { get; set; }
        string Description { get; set; }
    }
}
