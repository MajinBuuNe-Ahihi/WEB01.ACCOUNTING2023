using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;


namespace WEB01.ACCOUNTING2023.CORE.Attribute.Validate
{
    /// <summary>
    /// attribute custom validate vượt quá ngày hiện tại
    ///  create by: HV Manh 20/3/2023
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OverDateAttribute:System.Attribute
    {
        #region Field
        string _info;
        #endregion

        #region Contructor
        public OverDateAttribute(string info) { 
            _info = info;
        }
        #endregion
        #region Property

        public string Info { get
            {
                return new ResourceManager(typeof(Resource.Resource)).GetString(_info) +" "+ Resource.Resource.OverDate;
            }
        }
        #endregion
    }
}
