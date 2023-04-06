using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;


namespace WEB01.ACCOUNTING2023.CORE.Attribute.Validate
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OverDateAttribute:System.Attribute
    {
        string _info;
        public OverDateAttribute(string info) { 
            _info = info;
        }
        public string Info { get
            {
                return new ResourceManager(typeof(Resource.Resource)).GetString(_info) +" "+ Resource.Resource.OverDate;
            }
        }
    }
}
