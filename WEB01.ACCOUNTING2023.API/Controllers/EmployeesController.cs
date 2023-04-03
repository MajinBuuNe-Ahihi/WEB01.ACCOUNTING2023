using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Services;
using WEB01.ACCOUNTING2023.CORE.Resource;

namespace WEB01.ACCOUNTING2023.API.Controllers
{

    public class EmployeesController : BaseController<Employees>
    {
        #region Field
        IEmployeeServices _employeeServices;
        IEmployeeRepository _employeeRepository;
        ILogger<EmployeesController> _logger;
        #endregion

        #region Constructor
        public EmployeesController(IIfrastructureBase<Employees> ifrastructureBase,
            IEmployeeRepository employeeRepository,
            IEmployeeServices employeeServices,
            ILogger<EmployeesController> logger) : base(ifrastructureBase, logger)
        {
            _employeeServices = employeeServices;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        #endregion

        #region HTTPGET
        /// <summary>
        ///  lấy danh sách nhân viên dựa trên các tham số filter
        ///  create by: HV Mạnh (20/3/2021)
        /// </summary>
        /// <param name="pageSize">kích thước 1 page</param>
        /// <param name="pageNumber">page hiện tại</param>
        /// <param name="key">từ khóa cần tìm</param>
        /// <returns> httpresult</returns>
        [HttpGet("filter")]
        public IActionResult GetFilter(int pageSize, int pageNumber, string? key)
        {
            try
            {
                var value = _employeeRepository.GetFilterData(pageSize, pageNumber, key);
                return StatusCode(value.StatusCode, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(HttpContext.TraceIdentifier + " " + ex.Message);
                return StatusCode(500, new ResponseResult()
                {
                    Data = null,
                    StatusCode = 500,
                    Message = Resource.ServerError,
                    DevMessage = ex.Message,
                    DevCode = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        ///  lấy mã code mới không trùng để tạo nhân viên mới
        ///    create by: HV Mạnh (20/3/2021)
        /// </summary>
        /// <returns> httpresult</returns>
        [HttpGet("new-code")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var value = _employeeRepository.GetNewCode();
                return StatusCode(value.StatusCode, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(HttpContext.TraceIdentifier + " " + ex.Message);
                return StatusCode(500, new ResponseResult()
                {
                    Data = null,
                    StatusCode = 500,
                    Message = Resource.ServerError,
                    DevMessage = ex.Message,
                    DevCode = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        ///  từ mã code kiểm tra trùng mã
        ///    create by: HV Mạnh (20/3/2021)
        /// </summary>
        /// <param name="code">mã code nhân viên</param>
        /// <returns> httpresult</returns>
        [HttpGet("duplicate-code/{code}")]
        public IActionResult CheckDuplicateCode(string code)
        {
            try
            {
                var value = _employeeRepository.GetDataByCode(code);
                return StatusCode(value.StatusCode, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(HttpContext.TraceIdentifier + " " + ex.Message);
                return StatusCode(500, new ResponseResult()
                {
                    Data = null,
                    StatusCode = 500,
                    Message = Resource.ServerError,
                    DevMessage = ex.Message,
                    DevCode = HttpContext.TraceIdentifier
                });
            }
        }
        #endregion

        #region HTTPPOST
        /// <summary>
        ///  thêm nhân viên
        ///    create by: HV Mạnh (20/3/2021)
        /// </summary>
        /// <param name="employees">thông tin nhân viên</param>
        /// <returns> httpresult</returns>
        [HttpPost]
        public IActionResult InsertNewEmployee([FromBody] Employees employees)
        {
            try
            {
                var value =  _employeeServices.Insert(employees);
                return StatusCode(value.StatusCode, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(HttpContext.TraceIdentifier + " " + ex.Message);
                return StatusCode(500, new ResponseResult()
                {
                    Data = null,
                    StatusCode = 500,
                    Message = Resource.ServerError,
                    DevMessage = ex.Message,
                    DevCode = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// xóa  nhiều nhân viên
        ///   create by: HV Mạnh (20/3/2021)
        /// </summary>
        /// <param name="ids">chuỗi danh sách mã nhân viên</param>
        /// <returns> httpresult</returns>
        [HttpPost("delete")]
        public IActionResult DeleteEmployees([FromBody] string ids)
        {
            try
            {
                var value =  _employeeRepository.DeleteEmployees(ids);
                return StatusCode(value.StatusCode, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(HttpContext.TraceIdentifier + " " + ex.Message);
                return StatusCode(500, new ResponseResult()
                {
                    Data = null,
                    StatusCode = 500,
                    Message = Resource.ServerError,
                    DevMessage = ex.Message,
                    DevCode = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids">chuối danh sách mã nhân viên</param>
        /// <returns></returns>
        [HttpPost("export-excel")]
        public IActionResult ExportExcel([FromBody] string ids)
        {
            try
            {
            var stream = _employeeServices.ExportFile(ids);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee.xlsx");
            }
             catch (Exception ex)
            {
                _logger.LogError(HttpContext.TraceIdentifier + " " + ex.Message);
                return StatusCode(500, new ResponseResult()
                {
                    Data = null,
                    StatusCode = 500,
                    Message = Resource.ServerError,
                    DevMessage = ex.Message,
                    DevCode = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        ///  upload file excel
        /// </summary>
        /// <param name="file">file được upload</param>
        /// <returns></returns>
        [HttpPost("import-excel")]
        public IActionResult ImportExcel(IFormFile file)
        {
            try
            {
            var result = _employeeServices.ImportFile(file);
            return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(HttpContext.TraceIdentifier + " " + ex.Message);
                return StatusCode(500, new ResponseResult()
                {
                    Data = null,
                    StatusCode = 500,
                    Message = Resource.ServerError,
                    DevMessage = ex.Message,
                    DevCode = HttpContext.TraceIdentifier
                });
            }
        }
        #endregion

        #region HTTPPUT
        /// <summary>
        ///  sửa nhân viên
        ///    create by: HV Mạnh(20/3/2021)
        /// </summary>
        /// <param name = "employees" > thông tin nhân viên</param>
        /// <returns> httpresult</returns>
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Employees employees)
        {
            try
            {
                var value = _employeeServices.Update(employees,employees.EmployeeId);
                return StatusCode(201, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(HttpContext.TraceIdentifier + " " + ex.Message);
                return StatusCode(500, new ResponseResult()
                {
                    Data = null,
                    StatusCode = 500,
                    Message = Resource.ServerError,
                    DevMessage = ex.Message,
                    DevCode = HttpContext.TraceIdentifier
                });
            }
        }
        #endregion
    }
}
