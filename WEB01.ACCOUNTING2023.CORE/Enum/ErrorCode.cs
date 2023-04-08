using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB01.ACCOUNTING2023.CORE.Enum
{
    /// <summary>
    /// enum định nghĩa mã code cho client
    ///  create by: HV Mạnh (16/3/2023)
    /// </summary>
    public enum ErrorCode
    {
        // thành công
        SUCCESS = 0,

        // có lỗi
        FAIL = 1,

        // dữ liệu không hợp lệ
        INVALID=2,

        //không tìm thấy
        NOT_FOUND = 4,

        // lỗi máy chủ
        SERVER_FAIL = 5,
    }
}
