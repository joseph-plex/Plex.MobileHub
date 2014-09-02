﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.Reflection;

namespace Plex.MobileHub.ServiceLibrary.AccessService
{
    public class Insert : MethodStrategyBase<Object>
    {
        public static T Cast<T>(object o)
        {
            return (T)o;
        }
        public void Strategy(String typeName, object Entry)
        {
            var type =  Repositories.Keys.First(p => p.FullName == typeName);
            var insert = Repositories[type].GetType().GetMethod("Insert", new Type[]{type} );
            var castMethod = GetType().GetMethod("Cast").MakeGenericMethod(type);
            insert.Invoke(Repositories[type], new object[]{ Entry });
        }
    }
}