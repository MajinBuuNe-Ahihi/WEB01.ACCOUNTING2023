using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Attribute;
using WEB01.ACCOUNTING2023.CORE.Enum;

namespace WEB01.ACCOUNTING2023.CORE.Entities.DTO
{
    /// <summary>
    ///  đối tượng hiển thị nhân viên
    ///   create by: HV Manh 20/3/2023
    /// </summary>
    public class EmployeeExcelDTO
    {
        /// <summary>
        /// code nhân viên
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Mã nhân viên", ExcelHorizontal.LEFT, 20)]
            public string EmployeeCode { get; set; }

        /// <summary>
        ///  mã  phòng ban
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Mã phòng ban", ExcelHorizontal.LEFT, 40)]
            public Guid DepartmentId { get; set; }

        /// <summary>
        ///  tuổi nhân viên
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Giới tính", ExcelHorizontal.CENTER)]
            public Gender Gender { get ; set; }

        /// <summary>
        ///  tên nhân viên
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Tên nhân viên", ExcelHorizontal.LEFT, 30)]
            public string FullName { get; set; }

        /// <summary>
        ///  chức danh nhân viên
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Chức danh", ExcelHorizontal.LEFT, 30)]
            public string? PositionName { get; set; }

        /// <summary>
        ///  ngày sinh nhân viên
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Ngày sinh", ExcelHorizontal.CENTER)]
            public DateTime? DateOfBirth { get; set; }

        /// <summary>
        ///  số cmnd
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Căn cước công dân", ExcelHorizontal.LEFT, 30)]
            public string? IdentityNumber { get; set; }

        /// <summary>
        ///  ngày cấp cmnd
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Ngày cấp", ExcelHorizontal.CENTER)]
            public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// nơi cấp
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Nơi cấp", ExcelHorizontal.LEFT, 30)]
            public string? IdentityPlace { get; set; }

        /// <summary>
        ///  địa chỉ nhân viên
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Địa chỉ", ExcelHorizontal.LEFT, 30)]
            public string? Address { get; set; }

        /// <summary>
        ///  số điện thoại
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Số điện thoại", ExcelHorizontal.LEFT, 30)]
            public string? PhoneNumber { get; set; }

        /// <summary>
        ///  số điện thoại bàn
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Số điện thoại bàn", ExcelHorizontal.LEFT, 30)]
            public string? Fax { get; set; }

        /// <summary>
        ///  email nhân viên
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Email", ExcelHorizontal.LEFT, 30)]
            public string? Email { get; set; }

        /// <summary>
        ///  tài khoản ngân hàng
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Tài khoản ngân hàng", ExcelHorizontal.LEFT, 30)]
            public string? BankAccount { get; set; }

        /// <summary>
        ///  tên ngân hàng
        /// create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Tên ngân hàng", ExcelHorizontal.LEFT, 30)]
            public string? BankName { get; set; }

        /// <summary>
        ///  tên chi nhánh
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        [DescriptionExcel("Chi nhánh", ExcelHorizontal.LEFT, 40)]
            public string? BankBranch { get; set; }
    }
}
