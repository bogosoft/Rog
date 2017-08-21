using NUnit.Framework;
using Should;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rog.Tests
{
    [TestFixture, Category("Unit")]
    public class UnitTests
    {
        const int DefaultCount = 10000;

        static IRandomObjectGenerator Generator = RandomObjectGenerator.Default;

        object[] GenerateInstancesOf(Type type, int count = DefaultCount)
        {
            return GenerateInstancesOf(type, new Attribute[0], count);
        }

        object[] GenerateInstancesOf(Type type, IEnumerable<Attribute> attributes, int count = DefaultCount)
        {
            var instances = new object[count];

            for (var i = 0; i < count; i++)
            {
                instances[i] = Generator.Generate(type, attributes);
            }

            instances.Length.ShouldEqual(count);

            var subcount1 = instances.Count(x => !ReferenceEquals(null, x) && type.IsAssignableFrom(x.GetType()));
            var subcount2 = instances.Count(x => ReferenceEquals(null, x));

            (subcount1 + subcount2).ShouldEqual(count);

            if (type.IsValueType && Nullable.GetUnderlyingType(type) == null)
            {
                GenerateInstancesOf(typeof(Nullable<>).MakeGenericType(type), attributes, count);
            }

            return instances;
        }

        T[] GenerateInstancesOf<T>(int count = DefaultCount)
        {
            return GenerateInstancesOf<T>(new Attribute[0], count);
        }

        T[] GenerateInstancesOf<T>(IEnumerable<Attribute> attributes, int count = DefaultCount)
        {
            return GenerateInstancesOf(typeof(T), attributes, count).Cast<T>().ToArray();
        }

        [TestCase]
        public void AbstractionProviderThrowsArgumentExceptionWhenAbstractionTypeIsConcrete()
        {
            var provider = new AbstractionProvider();

            Action action = () => provider.Map<DefaultPlanet, GasGiant>();

            action.ShouldThrow<ArgumentException>();
        }

        [TestCase]
        public void CanGenerateArrayOfRandomDateTimes()
        {
            var arrays = GenerateInstancesOf<DateTime[]>(100);

            var total = arrays.Sum(x => x.Length);

            arrays.SelectMany(x => x).Count(x => x.GetType() == typeof(DateTime)).ShouldEqual(total);
        }

        [TestCase]
        public void CanGenerateDictionaryOfRandomKeyValuePairs()
        {
            var dictionary = Generator.Generate<Dictionary<int, DateTime>>();

            dictionary.ShouldBeType<Dictionary<int, DateTime>>();

            dictionary.Keys.Count(x => x.GetType() == typeof(int)).ShouldEqual(dictionary.Count);

            dictionary.Values.Count(x => x.GetType() == typeof(DateTime)).ShouldEqual(dictionary.Count);
        }

        [TestCase]
        public void CanGenerateDictionaryOfRandomKeyValuePairsUsingInterface()
        {
            var generator = RandomObjectGenerator.Default;

            generator.NullChance = 0;

            var dictionary = generator.Generate<IDictionary<byte, string>>();

            dictionary.ShouldBeType<Dictionary<byte, string>>();

            dictionary.Keys.Count(x => x.GetType() == typeof(byte)).ShouldEqual(dictionary.Count);

            dictionary.Values.Count(x => x.GetType() == typeof(string)).ShouldEqual(dictionary.Count);
        }

        [TestCase]
        public void CanGenerateInstancesOfComplexType()
        {
            GenerateInstancesOf<DefaultPlanet>();
        }

        [TestCase]
        public void CanGenerateInstancesOfComplexTypeUsingInterface()
        {
            var generator = RandomObjectGenerator.Default;

            generator.ValueProviders.Add(new AbstractionProvider().Map<IPlanet, DefaultPlanet>());

            var instances = generator.Generate<IPlanet>(DefaultCount);

            instances.Count(x => x.GetType() == typeof(DefaultPlanet));
        }

        [TestCase]
        public void CanGenerateListOfRandomGuids()
        {
            var list = Generator.Generate<List<Guid>>();

            list.Count(x => x.GetType() == typeof(Guid)).ShouldEqual(list.Count);
        }

        [TestCase]
        public void CanGenerateListOfRandomDateTimesUsingInterface()
        {
            var list = Generator.Generate<IList<DateTime>>();

            list.Count(x => x.GetType() == typeof(DateTime)).ShouldEqual(list.Count);
        }

        [TestCase]
        public void CanGenerateRandomEnumValues()
        {
            var counts = new Dictionary<PlanetSize, int>();

            var values = typeof(PlanetSize).GetEnumValues();

            foreach (var size in values.Cast<PlanetSize>())
            {
                counts[size] = 0;
            }

            var count = 10000;

            var sizes = GenerateInstancesOf<PlanetSize>(count);

            values.Cast<PlanetSize>()
                  .OrderBy(x => x.ToString())
                  .SequenceEqual(counts.Keys.OrderBy(x => x.ToString()))
                  .ShouldBeTrue();

            foreach (var size in sizes)
            {
                counts[size]++;
            }

            counts.Values.Sum().ShouldEqual(count);
        }

        [TestCase]
        public void CanGenerateRandomValueTypes()
        {
            var types = new Type[]
            {
                typeof(bool),
                typeof(byte),
                typeof(char),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(decimal),
                typeof(double),
                typeof(float),
                typeof(Guid),
                typeof(int),
                typeof(long),
                typeof(sbyte),
                typeof(short),
                typeof(TimeSpan),
                typeof(uint),
                typeof(ulong),
                typeof(ushort)
            };

            foreach (var type in types)
            {
                GenerateInstancesOf(type, 100000);
            }
        }

        [TestCase]
        public void CanGenerateSequenceOfRandomIntegers()
        {
            var integers = GenerateInstancesOf<IEnumerable<int>>(100);

            var total = integers.Sum(x => x.Count());

            integers.SelectMany(x => x).Count(x => x.GetType() == typeof(int)).ShouldEqual(total);
        }

        [TestCase]
        public void CanGenerateSequenceOfRandomKeyValuePairs()
        {
            var kvpairs = GenerateInstancesOf<IEnumerable<KeyValuePair<Guid, int>>>(100);

            var total = kvpairs.Sum(x => x.Count());

            var keys = kvpairs.SelectMany(x => x.Select(y => y.Key));

            keys.Count(x => x.GetType() == typeof(Guid)).ShouldEqual(total);

            var values = kvpairs.SelectMany(x => x.Select(y => y.Value));

            values.Count(x => x.GetType() == typeof(int)).ShouldEqual(total);
        }

        [TestCase]
        public void DefaultRandomObjectProviderThrowsArgumentExceptionWhenGivenTypeCannotBeMapped()
        {
            var generator = new DefaultRandomObjectGenerator(new DefaultRandomNumberGenerator());

            Action action = () => generator.Generate<bool>();

            action.ShouldThrow<ArgumentException>();
        }

        [TestCase]
        public void MaxLengthAttributeOnArraysIsHonored()
        {
            var maxlen = 8;

            var attributes = new Attribute[] { new MaxLengthAttribute(maxlen) };

            var arrays = GenerateInstancesOf<Guid[]>(attributes, 1000);

            arrays.Count(x => x.Length > maxlen).ShouldEqual(0);
        }

        [TestCase]
        public void MaxLengthAttributeOnDictionariesIsHonored()
        {
            var maxlen = 8;

            var attributes = new Attribute[] { new MaxLengthAttribute(maxlen) };

            var dictionaries = GenerateInstancesOf<IDictionary<byte, float>>(attributes, 1000);

            dictionaries.Count(x => x.Count > maxlen).ShouldEqual(0);

            dictionaries = GenerateInstancesOf<IDictionary<byte, float>>(attributes, 1000);

            dictionaries.Count(x => x.Count > maxlen).ShouldEqual(0);
        }

        [TestCase]
        public void MaxLengthAttributeOnIEnumerableIsHonored()
        {
            var maxlen = 8;

            var attributes = new Attribute[] { new MaxLengthAttribute(maxlen) };

            var sequence = GenerateInstancesOf<IEnumerable<short>>(attributes, 1000);

            sequence.Count(x => x.Count() > maxlen).ShouldEqual(0);
        }

        [TestCase]
        public void MaxLengthAttributeOnListsIsHonored()
        {
            var maxlen = 8;

            var attributes = new Attribute[] { new MaxLengthAttribute(maxlen) };

            var lists = GenerateInstancesOf<IList<float>>(attributes, 1000);

            lists.Count(x => x.Count > maxlen).ShouldEqual(0);

            lists = GenerateInstancesOf<List<float>>(attributes, 1000);

            lists.Count(x => x.Count > maxlen).ShouldEqual(0);
        }

        [TestCase]
        public void MaxLengthAttributeOnStringIsHonored()
        {
            var maxlen = 64;

            var attributes = new Attribute[]
            {
                new MaxLengthAttribute(maxlen),
                new MinLengthAttribute(48),
                new RequiredAttribute()
            };

            var strings = GenerateInstancesOf<string>(attributes, 10000);

            strings.Count(x => x.Length > maxlen).ShouldEqual(0);
        }

        [TestCase]
        public void MinLengthAttributeOnArraysIsHonored()
        {
            var minlen = 4;

            var attributes = new Attribute[] { new MinLengthAttribute(minlen) };

            var arrays = GenerateInstancesOf<DateTime[]>(attributes, 1000);

            arrays.Count(x => x.Length < minlen).ShouldEqual(0);
        }

        [TestCase]
        public void MinLengthAttributeOnDictionariesIsHonored()
        {
            var minlen = 4;

            var attributes = new Attribute[] { new MinLengthAttribute(minlen) };

            var lists = GenerateInstancesOf<IDictionary<float, string>>(attributes, 1000);

            lists.Count(x => x.Count < minlen).ShouldEqual(0);

            lists = GenerateInstancesOf<Dictionary<float, string>>(attributes, 1000);

            lists.Count(x => x.Count < minlen).ShouldEqual(0);
        }

        [TestCase]
        public void MinLengthAttributeOnIEnumerableIsHonored()
        {
            var minlen = 4;

            var attributes = new Attribute[] { new MinLengthAttribute(minlen) };

            var sequence = GenerateInstancesOf<IEnumerable<TimeSpan>>(attributes, 1000);

            sequence.Count(x => x.Count() < minlen).ShouldEqual(0);
        }

        [TestCase]
        public void MinLengthAttributeOnListsIsHonored()
        {
            var minlen = 4;

            var attributes = new Attribute[] { new MinLengthAttribute(minlen) };

            var lists = GenerateInstancesOf<IList<float>>(attributes, 1000);

            lists.Count(x => x.Count < minlen).ShouldEqual(0);

            lists = GenerateInstancesOf<List<float>>(attributes, 1000);

            lists.Count(x => x.Count < minlen).ShouldEqual(0);
        }

        [TestCase]
        public void MinLengthAttributeOnStringIsHonored()
        {
            var minlen = 32;

            var attributes = new Attribute[]
            {
                new MaxLengthAttribute(64),
                new MinLengthAttribute(minlen),
                new RequiredAttribute()
            };

            var strings = GenerateInstancesOf<string>(attributes, 10000);

            strings.Count(x => x.Length < minlen).ShouldEqual(0);
        }

        [TestCase]
        public void NullableValueTypesOccasionallyReceiveNullValues()
        {
            var generator = RandomObjectGenerator.Default;

            generator.NullChance = 0.20f;

            var events = GenerateInstancesOf<Event>();

            events.Count(x => ReferenceEquals(null, x.Id)).ShouldEqual(0);

            events.Count(x => x.CorrelationId == null).ShouldBeGreaterThan(0);
        }

        [TestCase]
        public void RequiredAttributeOnStringMemberIsHonored()
        {
            var people = GenerateInstancesOf<Person>();

            people.Count(x => x.Name != null).ShouldEqual(DefaultCount);

            people.Count(x => x.Alias != null).ShouldBeLessThan(DefaultCount);
        }
    }
}