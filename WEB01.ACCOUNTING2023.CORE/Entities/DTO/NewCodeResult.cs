using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB01.ACCOUNTING2023.CORE.Entities.DTO
{
    /// <summary>
    ///  định nghĩa đối tượng trả về một mã code mới
    ///  createby: HV Mạnh (16/3/2023)
    /// </summary>
    public class NewCodeResult
    {
        /// <summary>
        ///  mã code
        /// </summary>
        private string _code;
        private string _prefixString;
        
        public string Code
        {
            get => _code;
            set
            {
                int prefixNumberZero = (int.Parse(value) < 1000 && int.Parse(value) >= 100) ? 1 : (int.Parse(value) < 100 && int.Parse(value) >= 10) ? 2 : int.Parse(value) > 999?0:3;
                string prefixStringZero = "";
                for (int i = 0; i < prefixNumberZero; i++)
                {
                    prefixStringZero += "0";
                }
                _code = this._prefixString + "-" + prefixStringZero + value;
            }
        }

        public NewCodeResult(string prefixString)
        {
            _prefixString = prefixString;
        }
    }
}
