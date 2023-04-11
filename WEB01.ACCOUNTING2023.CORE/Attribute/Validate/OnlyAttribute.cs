using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Resource;
using System.Resources;

namespace WEB01.ACCOUNTING2023.CORE.Attribute.Validate
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OnlyAttribute:System.Attribute
    {
        #region Field
        string _propertyName;
        string _info;
       string _description;

        #endregion

        #region Constructor
        /// <summary>
        /// attribute custom validate tồn tại duy nhất
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        /// <param name="propertyName">tên property</param>
        public OnlyAttribute(string propertyName) { 
            _propertyName = propertyName;
            _description = new ResourceManager(typeof(Resource.Resource)).GetString(_propertyName);
            _info =  Resource.Resource.OnlyInvalid;
        }
        #endregion

        #region Property
        public string PropertyName { get { return _propertyName; } }
        public string Info{ get
            {
                return  _info;
            }
        }
        public string Description { get
            {
                return _description;
            } }
        #endregion
        

    }
}
