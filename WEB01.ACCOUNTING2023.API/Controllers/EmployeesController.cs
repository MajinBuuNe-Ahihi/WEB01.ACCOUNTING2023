using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Services;
using WEB01.ACCOUNTING2023.CORE.Resource;

namespace WEB01.ACCOUNTING2023.API.Controllers
{

    public class EmployeesController : BaseController<Employee>
    {
        #region Field
        IEmployeeServices _employeeServices;
        IEmployeeRepository _employeeRepository;
        ILogger<EmployeesController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        ///  constructor
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        /// <param name="ifrastructureBase">DI infrasturebas</param>
        /// <param name="employeeRepository">DI employeeRepositoty</param>
        /// <param name="employeeServices">DI employee Services</param>
        /// <param name="logger">DI logger</param>
        public EmployeesController(IIfrastructureBase<Employee> ifrastructureBase,
            IEmployeeRepository employeeRepository,
            IEmployeeServices employeeServices,
            ILogger<EmployeesController> logger) : base(ifrastructureBase, logger)
        {
            _employeeServices = employeeServices;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        #endregion

        #region Method

        #region HTTPGET
        /// <summary>
        ///  lấy danh sách nhân viên dựa trên các tham số filter
        ///  create by: HV Mạnh (20/3/2023)
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
        ///  lấy mã code mới không trùng để tạo nhân viên mớ
        ///    create by: HV Mạnh (20/3/2023)
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
        ///    create by: HV Mạnh (20/3/2023)
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
        ///    create by: HV Mạnh (20/3/2023)
        /// </summary>
        /// <param name="employee">thông tin nhân viên</param>
        /// <returns> httpresult</returns>
        [HttpPost]
        public IActionResult InsertNewEmployee([FromBody] Employee employee)
        {
            try
            {
                var value = _employeeServices.Insert(employee);
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
        ///   create by: HV Mạnh (20/3/2023)
        /// </summary>
        /// <param name="ids">chuỗi danh sách mã nhân viên</param>
        /// <returns> httpresult</returns>
        [HttpPost("delete")]
        public IActionResult DeleteEmployees([FromBody] string ids)
        {
            try
            {
                var value = _employeeRepository.DeleteEmployees(ids);
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
        ///  export file excel
        /// createBy: HV Mạnh (30/3/2023)
        /// </summary>
        /// <param name="ids">danh sách ids</param>
        /// <param name="key">từ khóa</param>
        /// <param name="type">kiểu export</param>
        /// <returns></returns>
        [HttpPost("export-excel/{type:regex(^(getall|byids)$)}")]
        public IActionResult ExportExcel([FromBody] string? ids, [FromQuery] string? key, [FromRoute] string type)
        {
            try
            {
                var stream = _employeeServices.ExportFile(ids, type, key);
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee.xlsx");
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
        ///   create by: HV Manh 6/4/2023
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
        ///    create by: HV Mạnh(20/3/2023)
        /// </summary>
        /// <param name = "employee" > thông tin nhân viên</param>
        /// <returns> httpresult</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee([FromBody] Employee employee,Guid?id)
        {
            try
            {
                var value = _employeeServices.Update(employee,id);
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
        #endregion
    }
}
