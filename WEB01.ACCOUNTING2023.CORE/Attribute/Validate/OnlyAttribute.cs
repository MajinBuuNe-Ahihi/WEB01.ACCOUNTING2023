using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Resource;

namespace WEB01.ACCOUNTING2023.CORE.Attribute.Validate
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OnlyAttribute:System.Attribute
    {
        #region Field
        string _propertyName;
        string _info;
        #endregion
        public OnlyAttribute(string propertyName) { 
            _propertyName = propertyName;
            _info = Resource.Resource.OnlyInvalid;
        }

        public string PropertyName { get { return _propertyName; } }
        public string Info{ get
            {
                return _propertyName +" " + _info;
            }
        }

    }
}
