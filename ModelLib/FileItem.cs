using ModelInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelLib
{
    [Serializable]
    public class FileItem : BaseModel, IFileItem
    {

        #region Property List<IPropertyItem> 'Properties'
        private List<IPropertyItem> _Properties = new List<IPropertyItem>();
        public List<IPropertyItem> Properties
        {
            get { return _Properties; }
        }
        #endregion


    }
}
