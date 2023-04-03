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
        SUCCESS = 0,
        FAIL = 1,
        INVALID=2,
        NOT_FOUND = 4,
        SERVER_FAIL = 5,
    }
}
