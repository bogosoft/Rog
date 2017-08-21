using System;
using System.Collections.Generic;

namespace Rog
{
    /// <summary>
    /// An implementation of the <see cref="IValueProvider"/> that can generate random key
    /// and value objects for a key-value pair.
    /// </summary>
    public class RandomKeyValuePairProvider : IValueProvider
    {
        /// <summary>
        /// Get a value from the current provider.
        /// </summary>
        /// <param name="context">
        /// The context within which a value will be generated.
        /// </param>
        /// <returns>A generated value.</returns>
        public object GetValue(GenerationContext context)
        {
            var argTypes = context.CurrentType.GetGenericArguments();

            var ctor = context.CurrentType.GetConstructor(argTypes);

            var args = new object[2];

            args[0] = context.Generate(argTypes[0]);
            args[1] = context.Generate(argTypes[1]);

            return ctor.Invoke(args);
        }

        /// <summary>
        /// Determine whether the curren value provider is capable of
        /// generating a value against a given type.
        /// </summary>
        /// <param name="type">A type to generate a value against.</param>
        /// <returns>
        /// True if the given type can be used to generate a value for; false otherwise.
        /// </returns>
        public bool Matches(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
        }
    }
}