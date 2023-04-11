using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Resource;

namespace WEB01.ACCOUNTING2023.CORE.Attribute.Validate
{
    /// <summary>
    /// attribute custom validate email
    ///  create by: HV Manh 20/3/2023
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class EmailAttribute:System.Attribute
     {  /// <summary>
        /// thông tin 
        /// </summary>
        string _info;
        public EmailAttribute() {
            _info = Resource.Resource.EmailInvalid;
         }
        public string Info { get { return _info; } }
    }
}
