using System;

namespace Rog
{
    class DelegatedRandomTypedValueProvider<T> : IValueProvider
    {
        Func<GenerationContext, T> provider;

        internal DelegatedRandomTypedValueProvider(Func<GenerationContext, T> provider)
        {
            this.provider = provider;
        }

        public object GetValue(GenerationContext context)
        {
            return provider.Invoke(context);
        }

        public bool Matches(Type type)
        {
            return type == typeof(T);
        }
    }
}