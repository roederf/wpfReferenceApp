using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI
{
    public class DataContextFactory : DependencyObject
    {

        #region AttachedDependencyProperty 'Interface'
        /// <summary>
        /// sets or gets the Dragable
        /// </summary>
        public static void SetInterface(UIElement element, Type value)
        {
            element.SetValue(InterfaceProperty, value);
        }
        public static Type GetInterface(UIElement element)
        {
            return (Type)element.GetValue(InterfaceProperty);
        }

        /// <summary>
        /// DependencyProperty Interface
        /// </summary>
        public static readonly DependencyProperty InterfaceProperty = DependencyProperty.RegisterAttached(
            "Interface",
            typeof(Type),
            typeof(DataContextFactory),
            new PropertyMetadata(null, InterfacePropertyChangedCallback)
        );
        private static void InterfacePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Type)
            {
                FrameworkElement el = sender as FrameworkElement;
                if (el != null)
                {
                    el.DataContext = ViewModelFactory.CreateViewModel(e.NewValue as Type);
                }
            }
            else
            {
                FrameworkElement el = sender as FrameworkElement;
                if (el != null)
                {
                    el.DataContext = null;
                }
            }
        }
        #endregion
        
    }
}
