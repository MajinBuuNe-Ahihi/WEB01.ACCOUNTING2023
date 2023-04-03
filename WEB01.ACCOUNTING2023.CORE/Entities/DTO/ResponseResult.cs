using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Enum;

namespace WEB01.ACCOUNTING2023.CORE.Entities.DTO
{
    /// <summary>
    ///  đối tượng kết quả respon
    /// </summary>
    public class ResponseResult
    {
        public int StatusCode { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string? Message { get; set; }
        public string? DevCode { get; set; }
        public string? DevMessage { get; set; }
        public object? MoreInfo { get; set; }
        public dynamic Data { get; set; }
    }
}
