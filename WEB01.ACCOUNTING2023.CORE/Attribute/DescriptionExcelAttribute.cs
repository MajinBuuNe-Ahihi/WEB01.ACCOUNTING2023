using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Enum;

namespace WEB01.ACCOUNTING2023.CORE.Attribute
{
    /// <summary>
    /// attribute định nghĩa tên header kích thước và lề cho các cột của excel
    ///  create by: HV Manh 20/3/2023
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DescriptionExcelAttribute : System.Attribute
    {
        #region Field
        /// <summary>
        /// thông tin
        /// </summary>
        string _info;

        /// <summary>
        /// căn giữa
        /// </summary>
        ExcelHorizontal _columnHorizontal;

        /// <summary>
        /// độ rộng
        /// </summary>
        int _width;
        #endregion
        #region Constructor
        public DescriptionExcelAttribute() { }
        public DescriptionExcelAttribute(string info)
        {
            _info = info;
        }

        /// <summary>
        /// constructor
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        /// <param name="info">thông tin </param>
        /// <param name="excelHorizontal">căn giữa</param>
        public DescriptionExcelAttribute(string info, ExcelHorizontal excelHorizontal)
        {
            _info = info;
            _columnHorizontal = excelHorizontal;
        }

        /// <summary>
        /// constructor
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        /// <param name="info">thông tin</param>
        /// <param name="columnHorizontal">căn giữa</param>
        /// <param name="width">độ rộng</param>
        public DescriptionExcelAttribute(string info, ExcelHorizontal columnHorizontal, int width) : this(info, columnHorizontal)
        {
            _width = width;
        }

        #endregion
        public string Info { get { return _info; } }
        public ExcelHorizontal ColumnHorizontal { get { return _columnHorizontal; } }
        public int Width { get { return _width; } }

    }
}
