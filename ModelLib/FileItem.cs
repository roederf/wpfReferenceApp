using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib
{
    public class FileItem : BaseModel
    {
        
        #region Property List<PropertyItem> 'Properties'
        private List<PropertyItem> _Properties = new List<PropertyItem>();
        public List<PropertyItem> Properties
        {
            get { return _Properties; }
        }
        #endregion


    }
}
