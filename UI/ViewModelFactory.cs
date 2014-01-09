using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class ViewModelFactory
    {
        static Dictionary<Type, Type> _dict = new Dictionary<Type, Type>();
        static Assembly viewModelAssembly;
        static Assembly interfaceAssembly;

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        public static void SetViewmodelAssembly(Assembly a)
        {
            viewModelAssembly = a;
        }

        public static void SetInterfaceAssembly(Assembly a)
        {
            interfaceAssembly = a;
        }

        public static void RegisterClassesFromFolder()
        {
            var viewmodels = GetTypesInNamespace(viewModelAssembly, "ReferenceApplication.ViewModel");
            var interfaces = GetTypesInNamespace(interfaceAssembly, "UI.Interfaces");

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

    }

}
