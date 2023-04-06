using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;

namespace WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures
{
    public interface IDepartmentRepository:IIfrastructureBase<Department>
    {
        /// <summary>
        ///  lấy danh sách phòng ban dự vào giá trị tìm kiếm
        ///  create by: HV Mạnh (16/3/2023)
        /// </summary>
        /// <param name="value">giá trị cần tìm kiếm</param>
        /// <returns></returns>
        public ResponseResult GetDataByFilterValue(string value);
    }
}
