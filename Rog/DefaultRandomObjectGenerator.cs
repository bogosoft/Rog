using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rog
{
    /// <summary>
    /// The default implementation of the <see cref="IRandomObjectGenerator"/> contract.
    /// </summary>
    public sealed class DefaultRandomObjectGenerator : IRandomObjectGenerator
    {
        static readonly Attribute Required = new RequiredAttribute();

        /// <summary>
        /// Get or set the encoding that should be used when generating random strings.
        /// </summary>
        public Encoding Encoding = Encoding.Unicode;

        /// <summary>
        /// Get or set the maximum length that any generated sequence can be.
        /// </summary>
        public int MaxSequenceLength = 32;

        /// <summary>
        /// Get or set the maximum length that any generated string can be.
        /// </summary>
        public int MaxStringLength = 256;

        /// <summary>
        /// Get or set the minimum length that any generated sequence can be.
        /// </summary>
        public int MinSequenceLength = 8;

        /// <summary>
        /// Get or set the minimum length that any generated string can be.
        /// </summary>
        public int MinStringLength = 16;

        /// <summary>
        /// Get a dictionary of chances that a nullable type will yield a null value.
        /// </summary>
        public readonly Dictionary<Type, Percentage> NullChances = new Dictionary<Type, Percentage>();

        IRandomNumberGenerator rng;

        /// <summary>
        /// Get a collection of value providers that the current generator will use
        /// when generating object data.
        /// </summary>
        public readonly List<IValueProvider> ValueProviders = new List<IValueProvider>();

        /// <summary>
        /// Create a new instance of the <see cref="DefaultRandomObjectGenerator"/> class.
        /// </summary>
        /// <param name="rng"></param>
        public DefaultRandomObjectGenerator(IRandomNumberGenerator rng)
        {
            this.rng = rng;
        }

        /// <summary>
        /// Generate an object against a given type and collection of attributes.
        /// </summary>
        /// <param name="type">The type of an object to generate.</param>
        /// <param name="attributes">
        /// Attributes that should be taken into consideration by the current
        /// generator when generating objects.
        /// </param>
        /// <returns>
        /// An object with random data assigned to its members.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown in the event that the given type cannot be mapped by the current
        /// generator's collection of value providers.
        /// </exception>
        public object Generate(Type type, IEnumerable<Attribute> attributes)
        {
            if (RollNullFor(type, attributes))
            {
                return null;
            }

            var context = new GenerationContext(rng, this)
            {
                AssociatedAttributes = attributes,
                CurrentType = type,
                Encoding = Encoding,
                MaxSequenceLength = MaxSequenceLength,
                MaxStringLength = MaxStringLength,
                MinSequenceLength = MinSequenceLength,
                MinStringLength = MinStringLength
            };

            foreach (var x in ValueProviders)
            {
                if (x.Matches(type))
                {
                    return x.GetValue(context);
                }
            }

            throw new ArgumentException($"No provider has been defined for, '{type}'.");
        }

        bool RollNullFor(Type type, IEnumerable<Attribute> attributes)
        {
            return NullChances.ContainsKey(type)
                && (!type.IsValueType || Nullable.GetUnderlyingType(type) != null)
                && !attributes.Contains(Required)
                && rng.NextInt32(0, 99) < 100 * NullChances[type];
        }
    }
}