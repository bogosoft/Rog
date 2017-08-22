using System;
using System.ComponentModel.DataAnnotations;

namespace Rog
{
    /// <summary>
    /// A base implementation of the <see cref="IValueProvider"/> contract that handles
    /// returning null values on occasion.
    /// </summary>
    public abstract class NullProviderBase : INullProvider, IValueProvider
    {
        /// <summary>
        /// Get or set the percentage chance that a null can be provided.
        /// </summary>
        public Percentage NullChance { get; set; }

        /// <summary>
        /// Get a value from the current provider.
        /// </summary>
        /// <param name="context">
        /// The context within which a value will be generated.
        /// </param>
        /// <returns>A generated value.</returns>
        public object GetValue(GenerationContext context)
        {
            if (!context.HasAttribute<RequiredAttribute>() && RollForNull(context))
            {
                return null;
            }
            else
            {
                return GetNonNullValue(context);
            }
        }

        /// <summary>
        /// When overridden in a derived class, returns a value that is not null.
        /// </summary>
        /// <param name="context">
        /// The context within which a value will be generated.
        /// </param>
        /// <returns>A generated non-null value.</returns>
        protected abstract object GetNonNullValue(GenerationContext context);

        /// <summary>
        /// Determine whether the curren value provider is capable of
        /// generating a value against a given type.
        /// </summary>
        /// <param name="type">A type to generate a value against.</param>
        /// <returns>
        /// True if the given type can be used to generate a value for; false otherwise.
        /// </returns>
        public abstract bool Matches(Type type);

        bool RollForNull(GenerationContext context)
        {
            return context.NextInt32(0, 99) < 100 * NullChance;
        }
    }
}