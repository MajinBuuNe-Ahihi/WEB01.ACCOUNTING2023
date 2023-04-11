using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;

namespace WEB01.ACCOUNTING2023.CORE.Interfaces.Services
{
    public interface IEmployeeServices
    {
        /// <summary>
        /// thêm dữ liệu
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="entity">đối tượng nhân viên cần thêm</param>
        public ResponseResult Insert(Employee entity);

        /// <summary>
        /// sửa dữ liệu
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="entity">đối tượng nhân viên cần thêm</param>
        public ResponseResult Update(Employee entity,Nullable<Guid> id);

        /// <summary>
        /// xuất excel
        ///   createby: HVManh (31/3/2023)
        /// </summary>
        /// <param name="ids">danh sách mã lỗi</param>
        /// <param name="type">type xuất khẩu</param>
        /// <param name="keyWord">từ khóa lọc xuất khẩu</param>
        /// <returns></returns>
        public byte[] ExportFile(string ids,string type,string keyWord);

        /// <summary>
        ///  nhập excek từ file
        ///  createby: HVManh (31/3/2023)
        /// </summary>
        /// <param name="file">file binary đầu vào</param>
        /// <returns></returns>
        public ResponseResult ImportFile(IFormFile file);
    }
}
