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
       /// thực hiện validate dữ liệu
       /// </summary>
       /// <typeparam name="T">type đối tượng </typeparam>
       /// <param name="entity">dữ liệu validate</param>
       /// <param name="id">id validate</param>
        public  void ValidateData<T>(T entity, Guid? id);
    }
}
