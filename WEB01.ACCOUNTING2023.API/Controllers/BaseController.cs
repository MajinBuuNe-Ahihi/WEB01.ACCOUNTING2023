using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Resource;
using WEB01.ACCOUNTING2023.INFRASTRUCTURE.Respository;

namespace WEB01.ACCOUNTING2023.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Field
        IIfrastructureBase<T> _ifrastructureBase;
        ILogger<BaseController<T>> _logger;
        #endregion

        #region Constructor
        public BaseController(IIfrastructureBase<T> ifrastructureBase,ILogger<BaseController<T>> logger)
        {
            _ifrastructureBase = ifrastructureBase;
            _logger = logger;
        }
        #endregion

        #region Methods
        /// <summary>
        /// thực hiện lấy tất cả bản ghi
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var value = _ifrastructureBase.GetRecords<T>();
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
        /// lấy thông tin bản ghi dựa vào id
        /// create by: HV Manh 20/3/2023
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetRecordByID(Guid id)
        {
            try
            {
                var value = _ifrastructureBase.GetRecordByID(id);
                return StatusCode(value.StatusCode, value);
            }catch (Exception ex)
            {
                _logger.LogError(HttpContext.TraceIdentifier + " "+ ex.Message);
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
        ///  xóa bản ghi dựa trên id
        ///   create by: HV Manh 20/3/2023
        /// </summary>
        /// <param name="id">id của bản gi</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteRecordByID(Guid id)
        {
            try
            {
                var value = _ifrastructureBase.DeleteRecordByID(id);
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
