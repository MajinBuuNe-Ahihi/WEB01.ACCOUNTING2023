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
        #region Field
        string _info;
        #endregion

        #region Constructor
        public RequiredAttribute() { }
        public RequiredAttribute(string info)
        {
            _info = info;
        }
        #endregion

        #region Property
        public string Info
        {
            get
            {
                return new ResourceManager(typeof(Resource.Resource)).GetString(_info) +" "+ Resource.Resource.Required;
            }
        }
        #endregion
    }
}
