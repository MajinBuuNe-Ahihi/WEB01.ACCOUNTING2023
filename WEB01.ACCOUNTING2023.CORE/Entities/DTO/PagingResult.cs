using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB01.ACCOUNTING2023.CORE.Entities.DTO
{
    public class PagingResult<T>
    {
        public int Count { get; set; }
        public T Result { get; set; }
    }
}
