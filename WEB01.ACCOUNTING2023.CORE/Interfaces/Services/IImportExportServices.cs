using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;

namespace WEB01.ACCOUNTING2023.CORE.Interfaces.Services
{
   public interface IImportExportServices<T> 
    {
        /// <summary>
        /// xuất file theo dữ liệu
        /// create by: HV Mạnh (30/3/2023)
        /// </summary>
        /// <param name="data">entity</param>
        /// <returns></returns>
        public byte[] ExportFile(List<T> data);

        /// <summary>
        /// nhập file dữ liệu
        /// </summary>
        /// <param name="file">file truyền vào</param>
        /// <param name="temporary">đối tượng khởi tạo temple</param>
        /// <returns></returns>
        public ResponseResult ImportFile(IFormFile file);
    }
}
