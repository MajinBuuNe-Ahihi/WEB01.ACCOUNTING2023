using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures
{
    /// <summary>
    ///  Interface đại diện các thao tác của database
    ///  createby: HVManh (13/3/2023)
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        ///  mở kết nối db
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public void Open();

        /// <summary>
        ///  đóng kết nối db
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public void Close();

        /// <summary>
        ///  trả về đối tượng db đã kết nối'
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <returns></returns>
        public DbConnection GetConnection();


        /// <summary>
        /// trả về danh sách parameter
        /// createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public List<string> GetVaribleProc(string procName);
    }
}
