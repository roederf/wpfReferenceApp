using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class ViewModelFactory
    {
        static Dictionary<Type, Type> _dict = new Dictionary<Type, Type>();

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
