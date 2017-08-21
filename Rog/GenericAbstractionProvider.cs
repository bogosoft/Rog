using System;
using System.Collections.Generic;

namespace Rog
{
    /// <summary>
    /// An implementation of the <see cref="IValueProvider"/> contract that convertes generic
    /// abstractions into generic concrete types.
    /// </summary>
    public class GenericAbstractionProvider : IValueProvider
    {
        /// <summary>
        /// Get a map of generic abstract to generic concrete types.
        /// </summary>
        public readonly Dictionary<Type, Type> TypeDefMap = new Dictionary<Type, Type>
        {
            { typeof(IDictionary<,>), typeof(Dictionary<,>) },
            { typeof(IList<>), typeof(List<>) }
        };

        /// <summary>
        /// Get a value from the current provider.
        /// </summary>
        /// <param name="context">
        /// The context within which a value will be generated.
        /// </param>
        /// <returns>A generated value.</returns>
        public object GetValue(GenerationContext context)
        {
            var abstractType = context.CurrentType.GetGenericTypeDefinition();

            var argTypes = context.CurrentType.GetGenericArguments();

            var concreteType = TypeDefMap[abstractType].MakeGenericType(argTypes);

            return context.Generate(concreteType, context.AssociatedAttributes);
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
            return type.IsGenericType
                && (type.IsAbstract || type.IsInterface)
                && TypeDefMap.ContainsKey(type.GetGenericTypeDefinition());
        }
    }
}