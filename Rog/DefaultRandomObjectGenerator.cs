﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rog
{
    /// <summary>
    /// The default implementation of the <see cref="IRandomObjectGenerator"/> contract.
    /// </summary>
    public sealed class DefaultRandomObjectGenerator : IRandomObjectGenerator
    {
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

        /// <summary>
        /// Set the chance that a null is generated by all null-capable value providers
        /// to a given percentage.
        /// </summary>
        /// <param name="nullChance">
        /// A value corresponding to the chance that any null-capable value provider
        /// associated with the current generator will generate a null.
        /// </param>
        /// <returns>The current random object generator.</returns>
        public DefaultRandomObjectGenerator SetGlobalNullChance(Percentage nullChance)
        {
            foreach (var x in ValueProviders.OfType<INullProvider>())
            {
                x.NullChance = nullChance;
            }

            return this;
        }

        /// <summary>
        /// Set the chance that any associated value providers of a given type will return
        /// a null to a given percentage.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the value provider(s) on which to set the null percentage chance.
        /// </typeparam>
        /// <param name="nullChance">
        /// A value corresponding to the chance that any null-capable value provider
        /// associated with the current generator will generate a null.
        /// </param>
        /// <returns>The current random object generator.</returns>
        public DefaultRandomObjectGenerator SetNullChanceFor<T>(Percentage nullChance) where T : INullProvider
        {
            foreach (var x in ValueProviders.OfType<T>())
            {
                x.NullChance = nullChance;
            }

            return this;
        }
    }
}