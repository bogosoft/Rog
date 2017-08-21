using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rog
{
    /// <summary>
    /// An implementation of the <see cref="IValueProvider"/> contract that generates lists
    /// of random length.
    /// </summary>
    public class ListProvider : IValueProvider
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

            if (context.HasAttribute<MinLengthAttribute>())
            {
                minlen = context.GetAttribute<MinLengthAttribute>().Length;
            }
            else
            {
                minlen = context.MinSequenceLength;
            }

            if (context.HasAttribute<MaxLengthAttribute>())
            {
                maxlen = context.GetAttribute<MaxLengthAttribute>().Length;
            }
            else
            {
                maxlen = context.MaxSequenceLength;
            }

            var list = Activator.CreateInstance(context.CurrentType);

            var itemType = context.CurrentType.GetGenericArguments()[0];

            var size = context.NextInt32(minlen, maxlen);

            var args = new object[1];

            var adder = context.CurrentType.GetMethod("Add");

            for (var i = 0; i < size; i++)
            {
                args[0] = context.Generate(itemType);

                adder.Invoke(list, args);
            }

            return list;
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
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}