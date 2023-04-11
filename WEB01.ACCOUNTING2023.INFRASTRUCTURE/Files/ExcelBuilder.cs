using Microsoft.AspNetCore.Http;
using OfficeOpenXml.Style;
using OfficeOpenXml;
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
using WEB01.ACCOUNTING2023.CORE.Resource;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures.Files;

namespace WEB01.ACCOUNTING2023.INFRASTRUCTURE.Files
{
    public class ExcelBuilder:IFileContext
    {
        #region Field
        IEmployeeRepository _employeeRepository;
        IDepartmentRepository _departmentRepository;
        #endregion
        #region Constructor
        public ExcelBuilder(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        #endregion
        public byte[] ExportFile<T,G>(List<T> data)
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage())
            {
                // đọc các tên các field cần export từ EmployeeDTO
                List<PropertyInfo> properties = typeof(G).GetProperties().ToList<PropertyInfo>();
                // lưu tên các attribute
                List<string> nameProperties = new List<string>();
                // vẽ header excel
                var workSheet = package.Workbook.Worksheets.Add("Employee");
                workSheet.Cells[1, 1].Value = "DANH SÁCH NHÂN VIÊN";
                int colHeader = 2;
                workSheet.Cells[3, 1].LoadFromText("STT");
                workSheet.Cells[3, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[3, 1].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                workSheet.Cells[3, 1].Style.Font.Color.SetColor(Color.White);
                workSheet.Cells[3, 1].Style.Font.Bold = true;
                workSheet.Cells[3, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                workSheet.Cells[3, 1].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Columns[1].Width = 5;
                workSheet.Columns[1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // vẽ các label của excel dựa vào prop
                properties.ForEach((PropertyInfo prop) =>
                {

                    if (prop != null)
                    {
                        var attribute = prop.GetCustomAttributes(true);
                        if (attribute != null)
                        {
                            nameProperties.Add(prop.Name);
                            foreach (var item in attribute)
                            {
                                if (item != null && item.GetType() == typeof(DescriptionExcelAttribute))
                                {
                                    DescriptionExcelAttribute obj = (DescriptionExcelAttribute)item;
                                    if (obj.Info != null)
                                    {
                                        workSheet.Cells[3, colHeader].LoadFromText(obj.Info);
                                        workSheet.Cells[3, colHeader].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        workSheet.Cells[3, colHeader].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        workSheet.Cells[3, colHeader].Style.Font.Color.SetColor(Color.White);
                                        workSheet.Cells[3, colHeader].Style.Font.Bold = true;
                                        workSheet.Cells[3, colHeader].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        workSheet.Cells[3, colHeader].Style.Border.Right.Color.SetColor(Color.Black);
                                        workSheet.Columns[colHeader].Width = obj.Width > 0 ? obj.Width : 20;
                                        workSheet.Columns[colHeader].Style.WrapText = true;
                                       // workSheet.Columns[colHeader].Style.Numberformat.Format = prop.Name;
                                        if (obj.ColumnHorizontal == ExcelHorizontal.LEFT)
                                        {
                                            workSheet.Columns[colHeader].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            workSheet.Columns[colHeader].Style.Indent = 2;
                                        }
                                        else
                                        {
                                            if (obj.ColumnHorizontal == ExcelHorizontal.RIGHT)
                                            {
                                                workSheet.Columns[colHeader].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                                workSheet.Columns[colHeader].Style.Indent = 2;

                                            }
                                            else
                                            {
                                                workSheet.Columns[colHeader].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                            }
                                        }
                                    }

                                }
                            }
                        }

                    }
                    colHeader++;
                });
                workSheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.Font.Size = 16;
                workSheet.Cells[1, 1, 1, colHeader - 1].Merge = true;
                workSheet.Cells[2, 1, 2, colHeader - 1].Merge = true;
                // vẽ dữ liệu lên các cell
                int indexRow = 4;
                data.ForEach((T item) =>
                {
                    workSheet.Cells[indexRow, 1].Value = indexRow - 3;
                    int index = 2;
                    nameProperties.ForEach((name) =>
                    {
                        T obj = (T)item;
                        var value = obj.GetType().GetProperty(name).GetValue(obj);
                        var type = obj.GetType().GetProperty(name).PropertyType;
                        if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
                        {
                            workSheet.Cells[indexRow, index].Style.Numberformat.Format = "dd/MM/yyyy";
                            workSheet.Cells[indexRow, index].Value = value /*!= null ? ((DateTime)value).ToString("dd/MM/yyyy") : ""*/;
                        }
                        if (type == typeof(Gender) || type == typeof(Nullable<Gender>))
                        {
                            var gender = (Gender)value == Gender.FEMALE ?Resource.Female :
                            (Gender)value == Gender.MALE ? Resource.Male : Resource.Other;

                            workSheet.Cells[indexRow, index].Value = gender.ToString();
                        }
                        else
                        {
                            if (name == "DepartmentId")
                            {
                                var result = _departmentRepository.GetRecordByID(new Guid(value.ToString()));
                                if (result.Data != null && ((Department)result.Data).DepartmentName != null)
                                {
                                    workSheet.Cells[indexRow, index].Value = ((Department)result.Data).DepartmentName;
                                    workSheet.Cells[indexRow, index].Style.Numberformat.Format = value.ToString();
                                }
                            }
                            else
                            {
                                workSheet.Cells[indexRow, index].Value = value;
                            }
                        }
                        workSheet.Cells[indexRow, index].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        index++;
                    });
                    indexRow++;
                });
                // lưu vào stream
                package.SaveAs(memoryStream);
            }
            byte[] byteStream = memoryStream.ToArray();
            memoryStream.Dispose();
            // trả về dữ liệu
            return byteStream;
        }

        public ResponseResult ImportFile(IFormFile file)
        {
            // khởi tạo list errors
            Dictionary<string, string> ListErrors = new Dictionary<string, string>();
            if (file == null)
            {
                return new ResponseResult()
                {
                    Data = null,
                    ErrorCode = ErrorCode.FAIL,
                    Message = Resource.Fail,
                    StatusCode = 400,
                    MoreInfo = ListErrors,
                };
            }
            else
            {
                // khởi tạo file
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    List<string> listName = new List<string>();
                    file.CopyTo(memoryStream);

                    using (ExcelPackage package = new ExcelPackage(memoryStream))
                    {
                        //khởi tạo đối tượng excel
                        var workBook = package.Workbook.Worksheets[0];
                        var row = workBook.Dimension.Rows;
                        var col = workBook.Dimension.Columns;
                        // lấy các key object thông qua các title
                        for (int i = 2; i <= col; i++)
                        {
                            var value = workBook.Columns[i].Style.Numberformat.Format;
                            listName.Add(value);
                        }

                        // đọc dữ liệu qua từng cell
                        for (int i = 4; i <= row; i++)
                        {
                            var employee = new Employee();
                            string codeEmployee = "";
                            string error = "";
                            string text = "";
                            try
                            {
                                for (int j = 2; j <= col; j++)
                                {
                                    PropertyInfo proInfo = employee.GetType().GetProperty(listName[j - 2]);
                                    var value = workBook.Cells[i, j].Value;
                                    if (listName[j - 2] == "EmployeeCode")
                                    {
                                        codeEmployee = value.ToString();
                                        var check = _employeeRepository.GetDataByCode(codeEmployee);
                                        if (check.Data != null)
                                        {
                                            error = listName[j - 2];
                                            text = Resource.DuplicateCode;
                                            throw new Exception();
                                        }
                                    }
                                    error = listName[j - 2];
                                    // kiểm tra dữ liệu convert đầu vào
                                    switch (proInfo.PropertyType.Name)
                                    {
                                        case "Guid":
                                            {
                                                if (listName[j - 2] == "DepartmentId")
                                                {
                                                    value = new Guid(workBook.Cells[i, j].Style.Numberformat.Format.ToString());
                                                }
                                                else
                                                {
                                                    value = new Guid(value.ToString());
                                                }
                                            }; break;
                                        case "Nullable`1":
                                        case "DateTime":
                                            {
                                                if (value != null && value.ToString().Length > 0)
                                                {
                                                    value = DateTime.FromOADate((double)value); break;
                                                }
                                                else
                                                {
                                                    value = null;
                                                }
                                                break;
                                            }
                                        case "Gender":
                                            Gender temple = (value.ToString().Trim() == Resource.Female.ToString().Trim() ?
                                            Gender.FEMALE : value.ToString().Trim() == Resource.Male.ToString().Trim() ?
                                            Gender.MALE : Gender.OTHER);
                                            value = temple;
                                            break;
                                        default: break;
                                    }
                                    proInfo.SetValue(employee, value, null);
                                }
                                employee.CreateBy = "HoangVanManh";
                                employee.ModifierBy = "HoangVanManh";
                                employee.CreateDate = DateTime.Now;
                                employee.ModifierDate = DateTime.Now;
                                // thêm vào database
                                var result = _employeeRepository.InsertData((Employee)employee);
                            }
                            catch (Exception ex)
                            {
                                ListErrors.Add(codeEmployee + "--" + DateTime.Now.Ticks, error + " " + text);
                            }
                        }
                        if ((row - ListErrors.Count) == 1)
                        {
                            return new ResponseResult()
                            {
                                Data = null,
                                ErrorCode = ErrorCode.FAIL,
                                Message = Resource.Fail,
                                StatusCode = 400,
                                MoreInfo = ListErrors,
                            };
                        }
                        return new ResponseResult() { Data = 1, ErrorCode = ErrorCode.SUCCESS, Message = Resource.Success, StatusCode = 200, MoreInfo = ListErrors };
                    }
                }
            }
        }
    }
}
