using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sandbox.Controls
{
    public class TreeViewItemEx : TreeViewItem
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeViewItemEx();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeViewItemEx;
        }

        #region DependencyProperty 'IsTopLevel'
        /// <summary>
        /// sets or gets the IsTopLevel
        /// </summary>
        public bool IsTopLevel
        {
            get { return (bool)this.GetValue(IsTopLevelProperty); }
            set { this.SetValue(IsTopLevelProperty, value); }
        }
        /// <summary>
        /// DependencyProperty IsTopLevel
        /// </summary>
        public static readonly DependencyProperty IsTopLevelProperty = DependencyProperty.Register(
            "IsTopLevel",
            typeof(bool),
            typeof(TreeViewItemEx),
            new PropertyMetadata(false)
        );
        #endregion
    }
}
