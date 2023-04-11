using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace WEB01.ACCOUNTING2023.CORE.Attribute.Validate
{
    /// <summary>
    /// attribute custom validate length
    ///  create by: HV Manh 20/3/2023
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LengthAttribute:System.Attribute
    {
        #region Field
        string _info;
        int _length;
        string _propertyName;
        #endregion

        #region Constructor
        /// <summary>
        /// constructor
        ///  create by: HV Manh 20/3/2023
        /// </summary>
        /// <param name="length">độ dài</param>
        /// <param name="propertyName">tên property</param>
        public LengthAttribute(int length,string propertyName)
            {
                _propertyName = propertyName;
                _length = length;
                _info = new ResourceManager(typeof(Resource.Resource)).GetString(_propertyName) + " " + Resource.Resource.LengthInvalid;
            }
        #endregion
        #region Property
        public string Info { get { return _info; } }
        public int Length { get { return _length; } }
        #endregion
    }
}
