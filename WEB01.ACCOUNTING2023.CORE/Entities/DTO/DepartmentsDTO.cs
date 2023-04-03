﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB01.ACCOUNTING2023.CORE.Entities.DTO
{ 
    /// <summary>
    ///  đối tượng hiển thị phòng ban
    /// </summary>
    public class DepartmentsDTO
    {
        /// <summary>
        ///   mã  phòng ban
        ///   createby: HVManh (13/3/2023)
        /// </summary>
        public Guid DepartmentId { get; set; }

        ///// <summary>
        /////  mã code phòng ban 
        /////  createby: HV mạnh (13/3/2023)
        ///// </summary>
        //public string DepartmentCode { get; set; }

        /// <summary>
        ///  tên phòng ban
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
