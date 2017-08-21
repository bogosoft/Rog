using System;
using System.ComponentModel.DataAnnotations;

namespace Rog
{
    /// <summary>
    /// An implementation of the <see cref="IValueProvider"/> contract that can generate
    /// array of a given type.
    /// </summary>
    public class TypedArrayProvider : IValueProvider
    {
        /// <summary>
        /// Get a value from the current provider.
        /// </summary>
        /// <param name="context">
        /// The context within which a value will be generated.
        /// </param>
        /// <returns>A generated value.</returns>
        public virtual object GetValue(GenerationContext context)
        {
            return GetValue(context, context.CurrentType.GetElementType());
        }

        /// <summary>
        /// Get a value for an element of a sequence from the current provider.
        /// </summary>
        /// <param name="context">
        /// The context within which a value will be generated.
        /// </param>
        /// <param name="itemType">The type of the element to generate.</param>
        /// <returns>A generated value.</returns>
        protected object GetValue(GenerationContext context, Type itemType)
        {
            int maxlen, minlen;

            if (context.HasAttribute<MaxLengthAttribute>())
            {
                maxlen = context.GetAttribute<MaxLengthAttribute>().Length;
            }
            else
            {
                maxlen = context.MaxSequenceLength;
            }

            if (context.HasAttribute<MinLengthAttribute>())
            {
                minlen = context.GetAttribute<MinLengthAttribute>().Length;
            }
            else
            {
                minlen = context.MinSequenceLength;
            }

            var size = context.NextInt32(minlen, maxlen);

            var array = Array.CreateInstance(itemType, size);

            for (var i = 0; i < size; i++)
            {
                array.SetValue(context.Generate(itemType), i);
            }

            return array;
        }

        /// <summary>
        /// Determine whether the curren value provider is capable of
        /// generating a value against a given type.
        /// </summary>
        /// <param name="type">A type to generate a value against.</param>
        /// <returns>
        /// True if the given type can be used to generate a value for; false otherwise.
        /// </returns>
        public virtual bool Matches(Type type)
        {
            return type.IsArray && type.GetElementType() != null;
        }
    }
}