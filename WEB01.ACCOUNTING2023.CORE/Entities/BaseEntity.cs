using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB01.ACCOUNTING2023.CORE.Entities
{
    public class BaseEntity
    {
        /// <summary>
        ///  người tạo
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// thời gian tạo
        /// createby: HVManh (13/3/2023)
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        ///  ngày sửa
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public DateTime? ModifierDate { get; set; }

        /// <summary>
        /// người sửa
        /// createby: HVManh (13/3/2023)
        /// </summary>
        public string? ModifierBy { get; set; }
    }
}
