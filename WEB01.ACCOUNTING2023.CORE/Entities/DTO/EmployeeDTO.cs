using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Attribute;
using WEB01.ACCOUNTING2023.CORE.Attribute.Validate;
using WEB01.ACCOUNTING2023.CORE.Enum;

namespace WEB01.ACCOUNTING2023.CORE.Entities.DTO
{
    /// <summary>
    ///  đối tượng hiển thị nhân viên
    ///   create by: HV Manh 13/3/2023
    /// </summary>
    public class EmployeeDTO
    {
        /// <summary>
        ///  mã nhân viên
        ///  createby: HVManh (13/3/2023)
        /// </summary>       
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// code nhân viên
        /// createby: HVManh (13/3/2023)
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        ///  mã  phòng ban
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        ///  tuổi nhân viên
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        ///  tên nhân viên
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///  chức danh nhân viên
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        ///  ngày sinh nhân viên
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        ///  số cmnd
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        ///  ngày cấp cmnd
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// nơi cấp
        /// createby: HVManh (13/3/2023)
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        ///  địa chỉ nhân viên
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        ///  số điện thoại
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        ///  số điện thoại bàn
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        ///  email nhân viên
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        ///  tài khoản ngân hàng
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        ///  tên ngân hàng
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        ///  tên chi nhánh
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string? BankBranch { get; set; }
    }
}
