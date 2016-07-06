using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HostDoctor.Diagnostics.Service.Helpers
{
    public static class AssemblyLoader
    {
        public static IEnumerable<Type> GetTypesInAssemblies<TBase>(IEnumerable<string> assembliesFullPath)
        {
            var typeKindPredicate = GetTypeKindPredicate<TBase>();
            return from path in assembliesFullPath
                   let ass = Assembly.LoadFile(path)
                   let types = ass.GetTypes()
                       from t in types
                       where typeKindPredicate(t)
                       select t;
        }

        private static Func<Type, bool> GetTypeKindPredicate<T>()
        {
            var t = typeof(T);
            return t.IsInterface
                ? (Func<Type, bool>)(_ => _.GetInterfaces().Contains(t))
                : (Func<Type, bool>)(_ => _.BaseType == t || _.BaseType.IsSubclassOf(t));
        }

        public static Assembly GetAssemblies(IEnumerable<string> locals)
        {
            throw null;
        }
    }
}
