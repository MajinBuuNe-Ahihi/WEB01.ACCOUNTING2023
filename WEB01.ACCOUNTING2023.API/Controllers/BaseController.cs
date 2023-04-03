﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Resource;

namespace WEB01.ACCOUNTING2023.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Field
        IIfrastructureBase<T> _ifrastructureBase;
        #endregion

        #region Constructor
        public BaseController(IIfrastructureBase<T> ifrastructureBase)
        {
            _ifrastructureBase = ifrastructureBase;
        }
        #endregion

        #region Methods
        /// <summary>
        /// lấy thông tin bản ghi dựa vào id
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetDataByID(Guid id)
        {
            try
            {
                var value = _ifrastructureBase.GetDataByID(id);
                return StatusCode(value.StatusCode, value);
            }catch (Exception ex)
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
        ///  xóa bản ghi dựa trên id
        /// </summary>
        /// <param name="id">id của bản gi</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteDataByID(Guid id)
        {
            try
            {
                var value = _ifrastructureBase.DeleteDataByID(id);
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