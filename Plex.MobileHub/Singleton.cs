using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.MobileHub
{
    public class Singleton<T> where T :new()
    {
        public static T Instance { get { return instance; } }
        private static T instance;
        private Singleton() 
        {
            instance = new T();
        }
    }
}