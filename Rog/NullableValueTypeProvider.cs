using System;

namespace Rog
{
    /// <summary>
    /// An implementation of the <see cref="IValueProvider"/> contract that can generate
    /// random values (including nulls) for nullable value types.
    /// </summary>
    public class NullableValueTypeProvider : IValueProvider
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
            if (context.RollForNull())
            {
                return null;
            }
            else
            {
                return context.Generate(
                    Nullable.GetUnderlyingType(context.CurrentType),
                    context.AssociatedAttributes
                    );
            }
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
            return type.IsValueType && Nullable.GetUnderlyingType(type) != null;
        }
    }
}