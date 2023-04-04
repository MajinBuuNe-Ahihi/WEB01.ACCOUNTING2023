using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;

namespace WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures
{
    public interface IIfrastructureBase<T>
    {
        /// <summary>
        ///  xóa dữ liệu theo id
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="id">mã của T</param>
        /// <returns></returns>
        public ResponseResult DeleteDataByID(Guid id);

        /// <summary>
        ///  lấy dữ liệu theo id
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="id">mã của T</param>
        /// <returns></returns>
        public  ResponseResult GetDataByID(Guid id);

        /// <summary>
        ///  lấy tất cả bản ghi
        ///  createby: HVManh (4/4/2023)
        /// </summary>
        /// <returns></returns>
        public ResponseResult GetAllData<G>();
    }
}
