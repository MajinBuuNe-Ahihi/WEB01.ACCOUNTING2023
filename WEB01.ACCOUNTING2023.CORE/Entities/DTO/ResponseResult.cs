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
    ///  create by HV Manh (20/3/2023)
    /// </summary>
    public class ResponseResult
    {
        // mã trạng thái
        public int StatusCode { get; set; }

        // mã lỗi
        public ErrorCode ErrorCode { get; set; }

        // thông tin lỗi
        public string? Message { get; set; }

        // mã lỗi cho dev
        public string? DevCode { get; set; }

        // thông tin lỗi cho dev
        public string? DevMessage { get; set; }

        // thông tin thêm
        public object? MoreInfo { get; set; }

        // dữ liệu
        public dynamic Data { get; set; }
    }
}
