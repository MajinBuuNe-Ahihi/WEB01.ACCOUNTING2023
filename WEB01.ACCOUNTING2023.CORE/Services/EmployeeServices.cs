using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Attribute.Validate;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Services;

namespace WEB01.ACCOUNTING2023.CORE.Services
{
    public class EmployeeServices: ServiceBase , IEmployeeServices
    {
        #region Field
        IEmployeeRepository  _employeeRepository;
        IImportExportServices<EmployeesDTO> _importExportServices;
        #endregion

        #region Constructor
        public EmployeeServices(IEmployeeRepository employeeRepository, IImportExportServices<EmployeesDTO> importExportServices) { 
            _employeeRepository = employeeRepository;
            _importExportServices = importExportServices;
        }
        #endregion

        #region Methods
        public  override void ValidateData<T>(T entity, Guid? id)
        {
           base.ValidateData<T>(entity,id);
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                var objects = prop.GetCustomAttributes(true);
                var hasKey = ListErrors.ContainsKey(prop.Name);
                if (objects != null && !hasKey)
                {
                    foreach (System.Attribute obj in objects)
                    {
                        if (obj == null) continue;
                        if (obj.GetType() == typeof(OnlyAttribute))
                        {
                            OnlyAttribute onlyAttribute = (OnlyAttribute)obj;
                            if (prop.GetValue(entity, null) != null)
                            {
                                if(onlyAttribute.PropertyName == "EmployeeCode")
                                {

                                    if (id != null)
                                    {
                                        var result = _employeeRepository.GetDataByID((Guid)id);
                                        if (result.Data != null)
                                        {
                                            var data = (Employees)result.Data;
                                            if (data .EmployeeCode != prop.GetValue(entity, null))
                                            {
                                                var value= _employeeRepository.GetDataByCode((string)prop.GetValue(entity, null));
                                                if ( value.Data != null)
                                                {
                                                    ListErrors.Add(prop.Name, onlyAttribute.Info);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var value = _employeeRepository.GetDataByCode((string)prop.GetValue(entity, null));
                                        if(value.Data != null) {
                                            ListErrors.Add(prop.Name, onlyAttribute.Info);
                                        }
                                    }
                                     
                                }
                            }
                        }
                    }
                }
            }
        }

        public ResponseResult Insert(Employees entity)
        {
           ValidateData<Employees>(entity,null);
            if(ListErrors.Count == 0)
            {
                var result = _employeeRepository.InsertData(entity);
                return result;
            }

            return new ResponseResult() { Data = null, ErrorCode = CORE.Enum.ErrorCode.INVALID, Message = Resource.Resource.InvalidData.ToString(), StatusCode = 400, MoreInfo = ListErrors };
        }

        public MemoryStream ExportFile(string ids)
        {
            var result = _employeeRepository.GetEmployeeByIDs(ids);
            var value =_importExportServices.ExportFile(result.Data);
            return value;
        }

        public ResponseResult Update(Employees entity, Guid? id)
        {
            ValidateData<Employees>(entity, id);
            if (ListErrors.Count == 0 && id != null)
            {
                var result = _employeeRepository.UpdateData(entity, (Guid)id);
                return result;
            }
            return new ResponseResult() { Data = null, ErrorCode = CORE.Enum.ErrorCode.INVALID, Message = Resource.Resource.InvalidData.ToString(), StatusCode = 400, MoreInfo = ListErrors };
        }

        public ResponseResult ImportFile(IFormFile file)
        {
            var result =  _importExportServices.ImportFile(file);
            return result;
        }

        #endregion

    }
}
