using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace WEB01.ACCOUNTING2023.CORE.Attribute.Validate
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LengthAttribute:System.Attribute
    {
            string _info;
            int _length;
            string _propertyName;
            public LengthAttribute(int length,string propertyName)
            {
                _propertyName = propertyName;
                _length = length;
                _info = new ResourceManager(typeof(Resource.Resource)).GetString(_propertyName) + " " + Resource.Resource.LengthInvalid;
            }
            public string Info { get { return _info; } }
            public int Length { get { return _length; } }
    }
}
