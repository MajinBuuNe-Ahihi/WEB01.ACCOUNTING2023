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
using WEB01.ACCOUNTING2023.CORE.Interfaces.Services;

namespace WEB01.ACCOUNTING2023.CORE.Services
{
    public class ServiceBase : IServicesBase
    {
        #region Property
        protected Dictionary<string,string> ListErrors { get; set; }
        #endregion

        #region Constructor
         public ServiceBase() { 
            ListErrors = new Dictionary<string,string>();
        }
        #endregion
        #region Method
        public  virtual void ValidateData<T>(T entity, Guid? id)
        {
            // lấy tất cả các prop
            PropertyInfo[] props = typeof(T).GetProperties();
            // duyệt qua danh sách props
            foreach (PropertyInfo prop in props)
            {
                // lấy thông tin các attribute custom thực hiện thao tác
                var objects = prop.GetCustomAttributes(true);
                var valueProp = prop.GetValue(entity, null);
                if (objects != null)
                {
                    foreach (System.Attribute obj in objects)
                    {
                        if (obj == null) continue;
                        if (obj.GetType() == typeof(RequiredAttribute))
                        {
                            RequiredAttribute requiredAttribute = (RequiredAttribute)obj;
                            if (valueProp == null || valueProp == "")
                            {
                                ListErrors.Add(prop.Name, requiredAttribute.Info);
                            }
                        }
                        if (obj.GetType() == typeof(LengthAttribute))
                        {
                            LengthAttribute lengthAttribute = (LengthAttribute)obj;
                            if (valueProp != null && valueProp != "" && valueProp.ToString().Length > lengthAttribute.Length )
                            {
                                ListErrors.Add(prop.Name, lengthAttribute.Info);
                            }
                        }
                        if (obj.GetType() == typeof(EmailAttribute))
                        {
                            EmailAttribute emailValidateAttribute = (EmailAttribute)obj;
                            try
                            {
                                if (valueProp != null)
                                {
                                    var email = new MailAddress(valueProp.ToString());
                                }
                            }
                            catch
                            {
                                ListErrors.Add(prop.Name, emailValidateAttribute.Info);
                            }
                        }
                        if (obj.GetType() == typeof(OverDateAttribute))
                        {
                            OverDateAttribute overDateAttribute = (OverDateAttribute)obj;
                            if (valueProp != null)
                            {
                                var date = valueProp;
                                var dateCurrent = DateTime.Now;
                                if (DateTime.Compare((DateTime)date, dateCurrent) == 1)
                                {
                                    ListErrors.Add(prop.Name, overDateAttribute.Info);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
