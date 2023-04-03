using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Resource;

namespace WEB01.ACCOUNTING2023.API.Controllers
{
    public class DepartmentsController : BaseController<Departments>
    {
        #region Field
        IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentsController(IIfrastructureBase<Departments> ifrastructureBase,IDepartmentRepository departmentRepository) : base(ifrastructureBase)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion

        #region Method

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var value = _departmentRepository.GetAllData();
                return StatusCode(value.StatusCode, value);
            }
            catch (Exception ex)
            {
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
