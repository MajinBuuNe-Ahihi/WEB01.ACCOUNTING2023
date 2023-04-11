using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;

namespace WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures.Files
{
    public interface IFileContext
    {
        /// <summary>
        /// xuất file theo dữ liệu
        /// create by: HV Mạnh (30/3/2023)
        /// </summary>
        /// <param name="data">entity</param>
        /// <returns></returns>
        public byte[] ExportFile<T, G>(List<T> data);

        /// <summary>
        /// nhập file dữ liệu
        /// create by: HV Mạnh (30/3/2023)
        /// </summary>
        /// <param name="file">file truyền vào</param>
        /// <returns></returns>
        public ResponseResult ImportFile(IFormFile file);
    }
}
