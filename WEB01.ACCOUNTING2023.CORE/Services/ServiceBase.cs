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
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                var objects = prop.GetCustomAttributes(true);
                if (objects != null)
                {
                    foreach (System.Attribute obj in objects)
                    {
                        if (obj == null) continue;
                        if (obj.GetType() == typeof(RequiredAttribute))
                        {
                            RequiredAttribute requiredAttribute = (RequiredAttribute)obj;
                            if (prop.GetValue(entity, null) == null || prop.GetValue(entity, null) == "")
                            {
                                ListErrors.Add(prop.Name, requiredAttribute.Info);
                            }
                        }
                        if (obj.GetType() == typeof(EmailAttribute))
                        {
                            EmailAttribute emailValidateAttribute = (EmailAttribute)obj;
                            try
                            {
                                if (prop.GetValue(entity, null) != null)
                                {
                                    var email = new MailAddress(prop.GetValue(entity, null).ToString());
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
                            if (prop.GetValue(entity, null) != null)
                            {
                                var date = prop.GetValue(entity, null);
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
