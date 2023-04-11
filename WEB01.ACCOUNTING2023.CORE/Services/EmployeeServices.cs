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
        IImportExportServices<EmployeeDTO> _importExportServices;
        #endregion

        #region Constructor
        public EmployeeServices(IEmployeeRepository employeeRepository, IImportExportServices<EmployeeDTO> importExportServices) { 
            _employeeRepository = employeeRepository;
            _importExportServices = importExportServices;
        }
        #endregion

        #region Methods
        /// <summary>
        ///  validate override employee
        ///  create by HV Manh (20/3/2023)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        public  override void ValidateData<T>(T entity, Guid? id)
        {// thực hiện validate base
           base.ValidateData<T>(entity,id);
            //lấy danh sách props
            PropertyInfo[] props = typeof(T).GetProperties();

            // duyệt từng props lấy attribute validate theo attribute
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
                                            var data = (Employee)result.Data;
                                            var entityEmployeeCode = prop.GetValue(entity, null);
                                            if (data .EmployeeCode.ToString() != entityEmployeeCode.ToString())
                                            {
                                                var value= _employeeRepository.GetDataByCode((string)prop.GetValue(entity, null));
                                                if ( value.Data != null)
                                                {
                                                    ListErrors.Add(prop.Name, onlyAttribute.Description + " <" + entityEmployeeCode.ToString()+"> "+onlyAttribute.Info);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var value = _employeeRepository.GetDataByCode((string)prop.GetValue(entity, null));
                                        if(value.Data != null) {
                                            ListErrors.Add(prop.Name, onlyAttribute.Description + " <" + (string)prop.GetValue(entity, null) + "> " + onlyAttribute.Info);
                                        }
                                    }
                                     
                                }
                            }
                        }
                    }
                }
            }
        }

        public ResponseResult Insert(Employee entity)
        {
            // validate
           ValidateData<Employee>(entity,null);
            // thao tác db
            if(ListErrors.Count == 0)
            {
                var result = _employeeRepository.InsertData(entity);
                return result;
            }
            // trả về dư liệu
            return new ResponseResult() { Data = null, ErrorCode = CORE.Enum.ErrorCode.INVALID, Message = Resource.Resource.InvalidData.ToString(), StatusCode = 400, MoreInfo = ListErrors };
        }

        public byte[] ExportFile(string ids, string type = "byids",string keyWord="")
        {
            // kiểm tra type
            if(type == "byids")
            {   // lấy dữ liệu từ db
                var result = _employeeRepository.GetEmployeeByIDs(ids);
                // thực hiện export
                var value =_importExportServices.ExportFile(result.Data);
                return value;
            }
            else
            {
                var result = _employeeRepository.GetEmployeeByKeyWord<EmployeeDTO>(keyWord);
                var value = _importExportServices.ExportFile(result.Data);
                return value;
            }

        }

        public ResponseResult Update(Employee entity, Guid? id)
        {
            // validate
            ValidateData<Employee>(entity, id);
            if (ListErrors.Count == 0 && id != null)
            {
                // thực hiện thao tác
                var result = _employeeRepository.UpdateData(entity, (Guid)id);
                return result;
            }
            // trả về dữ liệu
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
