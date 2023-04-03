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
    /// create by: HV Mạnh 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DescriptionExcelAttribute : System.Attribute
    {
        string _info;
        ExcelHorizontal _columnHorizontal;
        int _width;

        public DescriptionExcelAttribute() { }
        public DescriptionExcelAttribute(string info)
        {
            _info = info;
        }
        public DescriptionExcelAttribute(string info, ExcelHorizontal excelHorizontal)
        {
            _info = info;
            _columnHorizontal = excelHorizontal;
        }
        public DescriptionExcelAttribute(string info, ExcelHorizontal columnHorizontal, int width) : this(info, columnHorizontal)
        {
            _width = width;
        }

        public string Info { get { return _info; } }
        public ExcelHorizontal ColumnHorizontal { get { return _columnHorizontal; } }
        public int Width { get { return _width; } }

    }
}
