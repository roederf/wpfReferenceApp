using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Generates DataContext
    /// </summary>
    public class ViewModelFactory : DependencyObject
    {
        static Dictionary<Type, Type> _dict = new Dictionary<Type, Type>();
        static private object _parameter;

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        public static void RegisterViewModelParameter(object parameter)
        {
            _parameter = parameter;
        }

        public static void Register(Type viewmodelInterface, Type viewmodelImpl)
        {
            _dict[viewmodelInterface] = viewmodelImpl;
        }

        public static object CreateViewModel(Type t, object param)
        {
            return Activator.CreateInstance(t, param);
        }

        #region AttachedDependencyProperty 'Instance'
        /// <summary>
        /// sets or gets the Dragable
        /// </summary>
        public static void SetInstance(UIElement element, Type value)
        {
            element.SetValue(InstanceProperty, value);
        }
        public static Type GetInstance(UIElement element)
        {
            return (Type)element.GetValue(InstanceProperty);
        }

        /// <summary>
        /// DependencyProperty Instance
        /// </summary>
        public static readonly DependencyProperty InstanceProperty = DependencyProperty.RegisterAttached(
            "Instance",
            typeof(Type),
            typeof(ViewModelFactory),
            new PropertyMetadata(null, InstancePropertyChangedCallback)
        );
        private static void InstancePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Type)
            {
                FrameworkElement el = sender as FrameworkElement;
                if (el != null)
                {
                    el.DataContext = ViewModelFactory.CreateViewModel(e.NewValue as Type, _parameter);
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
