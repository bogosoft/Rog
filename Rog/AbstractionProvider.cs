using System;
using System.Collections.Generic;

namespace Rog
{
    /// <summary>
    /// An implementation of the <see cref="IValueProvider"/> contract used for
    /// mapping abstractions to more derived types. The more derived type does
    /// not have to be concrete itself.
    /// </summary>
    public class AbstractionProvider : IValueProvider
    {
        /// <summary>
        /// Get a map of abstract types to more derived types.
        /// </summary>
        public readonly Dictionary<Type, Type> TypeMap = new Dictionary<Type, Type>();

        /// <summary>
        /// Get a value from the current provider.
        /// </summary>
        /// <param name="context">
        /// The context within which a value will be generated.
        /// </param>
        /// <returns>A generated value.</returns>
        public object GetValue(GenerationContext context)
        {
            return context.Generate(TypeMap[context.CurrentType], context.AssociatedAttributes);
        }

        /// <summary>
        /// Create a mapping between an abstracted type and a concrete type. The concrete
        /// type must be derived from the abstract type.
        /// </summary>
        /// <typeparam name="TAbstract">
        /// The type of an abstraction to map.
        /// </typeparam>
        /// <typeparam name="TConcrete">
        /// The type of an object deriving from the abstract type.
        /// </typeparam>
        /// <returns>The current abstraction provider.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown in the event that the given abstraction type is neither abstract
        /// nor an interface.
        /// </exception>
        public AbstractionProvider Map<TAbstract, TConcrete>() where TConcrete : TAbstract
        {
            var abstractType = typeof(TAbstract);

            if (!abstractType.IsAbstract && !abstractType.IsInterface)
            {
                throw new ArgumentException($"{abstractType} must be abstract or an interface.");
            }

            TypeMap[abstractType] = typeof(TConcrete);

            return this;
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
            return (type.IsAbstract || type.IsInterface) && TypeMap.ContainsKey(type);
        }
    }
}