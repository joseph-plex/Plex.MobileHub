using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pmh.ServiceLibrary
{
    public class Singleton<T> where T : new()
    {
        public static T Instance { get { return instance; } }
        private static T instance;
        private Singleton()
        {
            instance = new T();
        }
    }
}
