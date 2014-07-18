using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI
{
    public class ViewModelFactory : DependencyObject
    {
        static Dictionary<Type, Type> _dict = new Dictionary<Type, Type>();
        

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        public static void RegisterInterfacesAndImplementations(Assembly viewModelAssembly, string viewModelNamespace, Assembly interfaceAssembly, string interfaceNamespace)
        {
            var viewmodels = GetTypesInNamespace(viewModelAssembly, viewModelNamespace);
            var interfaces = GetTypesInNamespace(interfaceAssembly, interfaceNamespace);

            foreach (var item in viewmodels)
            {
                var intf = interfaces.FirstOrDefault(i => i.Name == "I" + item.Name);
                if (intf != null)
                {
                    Register(intf, item);
                }
            }
            
        }

        public static void Register(Type viewmodelInterface, Type viewmodelImpl)
        {
            _dict[viewmodelInterface] = viewmodelImpl;
        }

        public static object CreateViewModel(Type t)
        {
            if (_dict.ContainsKey(t))
            {
                return Activator.CreateInstance(_dict[t]);
            }
            else
            {
                return null;
            }
        }

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
            typeof(ViewModelFactory),
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
