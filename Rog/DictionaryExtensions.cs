using System;
using System.Collections.Generic;
using System.Linq;

namespace Rog
{
    static class DictionaryExtensions
    {
        internal static void Add(this Dictionary<Type, ConstructorData> cache, Type type)
        {
            var data = type.GetConstructors()
                           .Select(x => new ConstructorData(x))
                           .OrderByDescending(x => x.ArgTypes.Length)
                           .First();

            cache.Add(type, data);
        }
    }
}