using System;

namespace Rog
{
    /// <summary>
    /// A set of static methods for working with types that implement
    /// the <see cref="IRandomObjectGenerator"/> interface.
    /// </summary>
    public static class RandomObjectGenerator
    {
        /// <summary>
        /// Get a random object generator with default configuration options.
        /// </summary>
        public static DefaultRandomObjectGenerator Default
        {
            get
            {
                var rog = new DefaultRandomObjectGenerator(new DefaultRandomNumberGenerator());

                rog.ValueProviders.Add(new NullableValueTypeProvider());
                rog.ValueProviders.Add(new RandomEnumValueProvider());
                rog.ValueProviders.Add(x => x.NextBoolean());
                rog.ValueProviders.Add(x => x.NextByte());
                rog.ValueProviders.Add(x => x.NextChar());
                rog.ValueProviders.Add(x => x.NextDateTime());
                rog.ValueProviders.Add(x => x.NextDateTimeOffset());
                rog.ValueProviders.Add(x => x.NextDecimal());
                rog.ValueProviders.Add(x => x.NextDouble());
                rog.ValueProviders.Add(x => x.NextFloat());
                rog.ValueProviders.Add(x => Guid.NewGuid());
                rog.ValueProviders.Add(x => x.NextInt16());
                rog.ValueProviders.Add(x => x.NextInt32());
                rog.ValueProviders.Add(x => x.NextInt64());
                rog.ValueProviders.Add(x => x.NextSByte());
                rog.ValueProviders.Add(x => x.NextTimeSpan());
                rog.ValueProviders.Add(x => x.NextUInt16());
                rog.ValueProviders.Add(x => x.NextUInt32());
                rog.ValueProviders.Add(x => x.NextUInt64());
                rog.ValueProviders.Add(new GenericAbstractionProvider());
                rog.ValueProviders.Add(new ListProvider());
                rog.ValueProviders.Add(new DictionaryProvider());
                rog.ValueProviders.Add(new RandomKeyValuePairProvider());
                rog.ValueProviders.Add(new RandomStringProvider());
                rog.ValueProviders.Add(new TypedArrayProvider());
                rog.ValueProviders.Add(new TypedEnumerableProvider());
                rog.ValueProviders.Add(new DefaultComplexTypeProvider());

                return rog;
            }
        }
    }
}