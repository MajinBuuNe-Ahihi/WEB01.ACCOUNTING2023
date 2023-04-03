using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Resource;

namespace WEB01.ACCOUNTING2023.CORE.Attribute.Validate
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class EmailAttribute:System.Attribute
    {

        string _info;
        public EmailAttribute() {
            _info = Resource.Resource.EmailInvalid;
         }
        public string Info { get { return _info; } }
    }
}
