using System;
using System.Linq;
using System.Reflection;

namespace Rog.Tests
{
    static class MemberInfoExtensions
    {
        internal static T GetAttribute<T>(this MemberInfo mi) where T : Attribute
        {
            return mi.GetCustomAttributes().Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
        }

        internal static bool HasAttribute<T>(this MemberInfo mi) where T : Attribute
        {
            return mi.GetCustomAttributes().Any(x => x.GetType() == typeof(T));
        }
    }
}