using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;

namespace WEB01.ACCOUNTING2023.CORE.Interfaces.Services
{
    public interface IServicesBase
    {
        /// <summary>
        ///  kiểm tra dữ liệu
        /// </summary>
        /// <param name="data">đầu vào dữ liệu</param>
        /// <returns>validate hợp lê thự hiện luôn thao tác với đb và trả về response</returns>
        public  void ValidateData<T>(T entity, Guid? id);
    }
}
