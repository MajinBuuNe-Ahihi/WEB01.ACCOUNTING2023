using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.ConditionalFormatting;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Attribute;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;
using WEB01.ACCOUNTING2023.CORE.Enum;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Services;

namespace WEB01.ACCOUNTING2023.CORE.Services
{
    public class ExcelServices<T,G> :IImportExportServices<T>
    {
        #region Field
        IEmployeeRepository _employeeRepository;
        #endregion
        #region Constructor
        public ExcelServices(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion
        public MemoryStream ExportFile(List<T> data)
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage())
            {
                // đọc các tên các field cần export từ EmployeeDTO
                List<PropertyInfo> properties = typeof(G).GetProperties().ToList<PropertyInfo>();
                // lưu tên các attribute
                List<string> nameProperties = new List<string>();
                // tên address các cột
                List<string> nameAddress = new List<string>();
                // tạo header
                var workSheet = package.Workbook.Worksheets.Add("Employee");
                int asciiCode = 65;
                int level = 1; // cấp côt của excel ví a-z 1 cấp  aa-zz -> 1 cấp
                int indexColumn = 1;
                properties.ForEach((PropertyInfo prop) =>
                {
                    nameProperties.Add(prop.Name);
                    string nameHeader = "";
                    for (int i = 0; i < level; i++)
                    {
                        nameAddress.Add(nameHeader + Char.ToString((char)asciiCode));
                        nameHeader = nameHeader + Char.ToString((char)asciiCode) + "1";
                    }
                    asciiCode = asciiCode % 90 == 0 ? 65 : asciiCode + 1;
                    if (nameHeader != null && prop != null)
                    {
                        var attribute = prop.GetCustomAttributes(true);
                        if (attribute != null)
                        {
                            foreach (var item in attribute)
                            {
                                if (item != null && item.GetType() == typeof(DescriptionExcelAttribute))
                                {
                                    DescriptionExcelAttribute obj = (DescriptionExcelAttribute)item;
                                    if (obj.Info != null)
                                    {
                                        workSheet.Cells[nameHeader].LoadFromText(obj.Info);
                                        workSheet.Cells[nameHeader].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        workSheet.Cells[nameHeader].Style.Fill.BackgroundColor.SetColor(Color.SlateGray);
                                        workSheet.Cells[nameHeader].Style.Font.Bold = true;
                                        workSheet.Cells[nameHeader].AddComment((prop.Name).ToString());
                                        workSheet.Columns[indexColumn].Width = obj.Width > 0 ? obj.Width : 20;
                                        if (obj.ColumnHorizontal == ExcelHorizontal.LEFT)
                                        {
                                            workSheet.Columns[indexColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                        }
                                        else
                                        {
                                            if (obj.ColumnHorizontal == ExcelHorizontal.RIGHT)
                                            {
                                                workSheet.Columns[indexColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                            }
                                            else
                                            {
                                                workSheet.Columns[indexColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                            }
                                        }
                                    }

                                }
                            }
                        }

                    }
                    indexColumn++;
                });
                // thêm dữ liệu
                int indexRow = 2;
                data.ForEach((T item) =>
                {
                    int index = 0;
                    nameProperties.ForEach((name) =>
                    {
                        T obj = (T)item;
                        var value = obj.GetType().GetProperty(name).GetValue(obj);
                        var type = obj.GetType().GetProperty(name).PropertyType;
                        if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
                        {
                            workSheet.Cells[nameAddress[index] + indexRow.ToString()].Style.Numberformat.Format = "dd/MM/yyyy";
                            workSheet.Cells[nameAddress[index] + indexRow.ToString()].Value = value /*!= null ? ((DateTime)value).ToString("dd/MM/yyyy") : ""*/;
                        }
                        if (type == typeof(Gender) || type == typeof(Nullable<Gender>))
                        {
                            workSheet.Cells[nameAddress[index] + indexRow.ToString()].Value = ((Gender)value == Gender.FEMALE?"Nữ":(Gender)value == Gender.MALE?"Nam":"Khác");
                        }
                        else
                        {
                            workSheet.Cells[nameAddress[index] + indexRow.ToString()].Value = value;
                        }

                        index++;
                    });
                    indexRow++;
                });
                // lưu vào stream
                package.SaveAs(memoryStream);
            }
            return memoryStream;
        }

        public ResponseResult ImportFile(IFormFile file)
        {
            Dictionary<string, string> ListErrors = new Dictionary<string, string>();
            if (file == null)
            {
                return new ResponseResult()
                {
                    Data = null,
                    ErrorCode =ErrorCode.FAIL,
                    Message = Resource.Resource.Fail,
                    StatusCode =400,
                    MoreInfo = ListErrors,
                };
            }
            else
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    List<string> listName = new List<string>();
                    file.CopyTo(memoryStream);
                    using (ExcelPackage package = new ExcelPackage(memoryStream))
                    {
                        var workBook = package.Workbook.Worksheets[0];
                        var row = workBook.Dimension.Rows;
                        var col = workBook.Dimension.Columns;
                        for (int i = 1; i <= col; i++)
                        {
                            var value = workBook.Cells[1, i].Comment.Text;
                            listName.Add(value);
                        }

                        for (int i = 2; i <= row; i++)
                        {
                            var employee = new Employees();
                            string codeEmployee = "";
                            string error = "";
                            string text = "";
                            try
                            {
                            for (int j = 1; j <= col; j++)
                            {
                                PropertyInfo proInfo = employee.GetType().GetProperty(listName[j - 1]);
                                var value = workBook.Cells[i, j].Value;
                                if (listName[j - 1] == "EmployeeCode")
                                {
                                       codeEmployee = value.ToString();
                                       var check = _employeeRepository.GetDataByCode(codeEmployee);
                                        if (check.Data != null) {
                                            error = listName[j - 1];
                                            text = Resource.Resource.DuplicateCode;
                                            throw new Exception();
                                        }
                                }
                                    error = listName[j - 1];
                                switch (proInfo.PropertyType.Name)
                                {
                                    case "Guid":  value = new Guid(value.ToString()); break;
                                    case "Nullable`1":
                                    case "DateTime":
                                        {

                                            if (value != null && value.ToString().Length > 0)
                                            {
                                                value =DateTime.FromOADate((double)value); break;
                                               }
                                            else
                                            {
                                                value = null;
                                            }
                                            break;
                                        }
                                    case "Gender": value = (value == "Nữ" ? Gender.FEMALE : value == "Nam" ? Gender.MALE : Gender.OTHER); break;
                                    default: break;
                                    }
                                    proInfo.SetValue(employee, value, null);
                                }
                                var result =  _employeeRepository.InsertData((Employees)employee);
                            }catch(Exception ex)
                            {
                                ListErrors.Add(codeEmployee + "--"+ DateTime.Now.Ticks, error + " " + text);
                            }
                        }
                        if((row - ListErrors.Count) ==1)
                        {
                           return new ResponseResult()
                            {
                                Data = null,
                                ErrorCode = ErrorCode.FAIL,
                                Message = Resource.Resource.Fail,
                                StatusCode = 400,
                                MoreInfo = ListErrors,
                            };
                        }
                        return new ResponseResult() { Data = 1, ErrorCode = ErrorCode.SUCCESS, Message = Resource.Resource.Success, StatusCode = 200, MoreInfo = ListErrors };
                    }
                }
            }
        }
    }
}
