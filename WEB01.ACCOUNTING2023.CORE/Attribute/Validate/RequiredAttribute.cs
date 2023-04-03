using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace WEB01.ACCOUNTING2023.CORE.Attribute.Validate
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredAttribute : System.Attribute
    {
        string _info;
        public RequiredAttribute() { }
        public RequiredAttribute(string info)
        {
            _info = info;
        }
        public string Info
        {
            get
            {
                return _info + Resource.Resource.Required;
            }
        }
    }
}
