using System;
using System.ComponentModel.DataAnnotations;

namespace Rog
{
    /// <summary>
    /// An implementation of the <see cref="IValueProvider"/> contract that can generate strings
    /// of random length and content. This class honors the <see cref="MaxLengthAttribute"/>
    /// and <see cref="MinLengthAttribute"/> attributes.
    /// </summary>
    public class RandomStringProvider : IValueProvider
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
            int maxlen, minlen;

            if (context.HasAttribute<MaxLengthAttribute>())
            {
                maxlen = context.GetAttribute<MaxLengthAttribute>().Length;
            }
            else
            {
                maxlen = context.MaxStringLength;
            }

            if (context.HasAttribute<MinLengthAttribute>())
            {
                minlen = context.GetAttribute<MinLengthAttribute>().Length;
            }
            else
            {
                minlen = context.MinStringLength;
            }

            var buffer = new byte[context.NextInt32(minlen, maxlen) * 2];

            context.GetBytes(buffer);

            return context.Encoding.GetString(buffer);
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
            return type == typeof(string);
        }
    }
}