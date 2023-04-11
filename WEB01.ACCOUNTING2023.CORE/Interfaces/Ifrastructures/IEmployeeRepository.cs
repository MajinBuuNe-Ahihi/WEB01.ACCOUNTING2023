using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;

namespace WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures
{
    public interface IEmployeeRepository:IIfrastructureBase<Employee>
    {
        /// <summary>
        ///  lấy danh  mã code mới
        ///  author: HV Mạnh
        ///  createdate: 13/3/2023
        /// </summary>
        /// <returns>mã code</returns>
        public ResponseResult GetNewCode();

        /// <summary>
        ///  lấy danh sách đối tượng theo từ khóa và số bản ghi
        ///  author: HV Mạnh
        ///  createdate: 13/3/2023
        /// </summary>
        /// <param name="pageSize">kích thước 1 trang</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="key">từ khóa</param>
        /// <returns></returns>
        public ResponseResult GetFilterData(int pageSize, int pageNumber, string? key);

        /// <summary>
        ///  lấy dữ liệu số lượng bản ghi dựa vào mã code
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="code">mã code nhân viên</param>
        /// <returns></returns>
        public ResponseResult GetDataByCode(string code);

        /// <summary>
        ///  xóa nhiều
        /// </summary>
        /// <param name="ids"> string chứa nhiều id</param>
        /// <returns></returns>
        public ResponseResult DeleteEmployees(string ids);

        /// <summary>
        ///  lấy thông tin nhân viên thông qua danh sách id
        /// </summary>
        /// <param name="ids">id những nhân viên cần lấy thông tin</param>
        /// <returns></returns>
        public ResponseResult GetEmployeeByIDs(string ids);

        /// <summary>
        ///  thêm dữ liệu
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="data">Đố tượng nhân viên</param>
        /// <returns>số dòng tác động</returns>
        public ResponseResult InsertData(Employee data);

        /// <summary>
        ///  cập nhật data theo id
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="data">đối tượng nhân viên</param>
        /// <param name="id">mã của nhân viên</param>
        /// <returns></returns>
        public ResponseResult UpdateData(Employee data, Guid id);

        /// <summary>
        ///  lấy danh sach nhân viên dựa trên keyword
        ///  create by: HV Mạnh (9/4/2023)
        /// </summary>
        /// <param name="keyWord">từ khóa</param>
        /// <returns></returns>
        public ResponseResult GetEmployeeByKeyWord<T>(string keyWord);
    }
}
