using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB01.ACCOUNTING2023.CORE.Entities.DTO
{
    public class PagingResult<T>
    {
        /// <summary>
        /// số bản ghi
        /// </summary>
        public int Count { get; set; } 

        /// <summary>
        /// số kết quả
        /// </summary>
        public T Result { get; set; }
    }
}
