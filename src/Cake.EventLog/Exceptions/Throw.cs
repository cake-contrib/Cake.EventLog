using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.EventLog.Exceptions
{
    internal static class Throw<T> where T : Exception
    {
        internal static void IfNull(object o, string name)
        {
            if (o == null) throw CreateException(name);
        }

        internal static void If(Func<bool> func, string name = null)
        {
            if (func.Invoke()) throw CreateException(name);
        }

        private static Exception CreateException(string arg)
        {
            return Activator.CreateInstance(typeof(T), arg) as Exception;
        }
    }
}
