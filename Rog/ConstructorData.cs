using System;
using System.Linq;
using System.Reflection;

namespace Rog
{
    struct ConstructorData
    {
        internal readonly Type[] ArgTypes;

        internal readonly Constructor Constructor;

        internal ConstructorData(ConstructorInfo ci)
        {
            ArgTypes = ci.GetParameters().Select(x => x.ParameterType).ToArray();
            Constructor = ci.Invoke;
        }
    }
}