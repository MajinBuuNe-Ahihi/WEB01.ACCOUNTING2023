using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Resource;

namespace WEB01.ACCOUNTING2023.API.Controllers
{
    public class DepartmentsController : BaseController<Department>
    {
        #region Field
        IDepartmentRepository _departmentRepository;
        ILogger<DepartmentsController> _logger;
        #endregion

        #region Constructor
        public DepartmentsController(IIfrastructureBase<Department> ifrastructureBase,
            IDepartmentRepository departmentRepository,
            ILogger<DepartmentsController> logger) : base(ifrastructureBase,logger)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
        }
        #endregion

        #region Method
        /// <summary>
        ///  lọc phòng ban theo key
        ///   create by: HV Mạnh (20/3/2023)
        /// </summary>
        /// <param name="key">từ khóa lọc</param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult GetFilter(string key)
        {
            try
            {
                var value = _departmentRepository.GetDataByFilterValue(key);
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
    }
}
