using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rog
{
    /// <summary>
    /// An implementation of the <see cref="IValueProvider"/> contract that can create dictionaries
    /// of random size and contents. This provider honors the <see cref="MaxLengthAttribute"/>
    /// and <see cref="MinLengthAttribute"/> attributes.
    /// </summary>
    public sealed class DictionaryProvider : IValueProvider
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
            var dictionary = Activator.CreateInstance(context.CurrentType);

            var method = context.CurrentType.GetMethod("set_Item");

            var argTypes = context.CurrentType.GetGenericArguments();

            var args = new object[2];

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

            var size = context.NextInt32(minlen, maxlen);

            for (var i = 0; i < size; i++)
            {
                args[0] = context.Generate(argTypes[0]);
                args[1] = context.Generate(argTypes[1]);

                method.Invoke(dictionary, args);
            }

            return dictionary;
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
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }
    }
}