using System;
using System.Collections.Generic;

namespace Rog
{
    /// <summary>
    /// Extended functionality for the <see cref="IRandomObjectGenerator"/> contract.
    /// </summary>
    public static class RandomObjectGeneratorExtensions
    {
        /// <summary>
        /// Generate an object of a given type.
        /// </summary>
        /// <param name="generator">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <param name="type">The type of the object to generate.</param>
        /// <returns>
        /// An object of the given type with random data assigned to its members.
        /// </returns>
        public static object Generate(this IRandomObjectGenerator generator, Type type)
        {
            return generator.Generate(type, new Attribute[0]);
        }

        /// <summary>
        /// Generate a collection of objects of a given type.
        /// </summary>
        /// <param name="generator">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <param name="type">The type of the objects to create.</param>
        /// <param name="count">
        /// A value corresponding to the number of objects to create.
        /// </param>
        /// <returns>
        /// A collection of zero or more objects of the given type
        /// with random data assigned to their members.
        /// </returns>
        public static IEnumerable<object> Generate(
            this IRandomObjectGenerator generator,
            Type type,
            int count
            )
        {
            return generator.Generate(type, new Attribute[0], count);
        }

        /// <summary>
        /// Generate a collection of objects of a given type.
        /// </summary>
        /// <param name="generator">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <param name="type">The type of the objects to create.</param>
        /// <param name="attributes">
        /// Attributes that should be taken into consideration by the current
        /// generator when generating objects.
        /// </param>
        /// <param name="count">
        /// A value corresponding to the number of objects to create.
        /// </param>
        /// <returns>
        /// A collection of zero or more objectsof the given type
        /// with random data assigned to their members.
        /// </returns>
        public static IEnumerable<object> Generate(
            this IRandomObjectGenerator generator,
            Type type,
            IEnumerable<Attribute> attributes,
            int count
            )
        {
            for (var i = 0; i < count; i++)
            {
                yield return generator.Generate(type, attributes);
            }
        }

        /// <summary>
        /// Generate an object of a given type.
        /// </summary>
        /// <typeparam name="T">The type of the object to create.</typeparam>
        /// <param name="generator">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>
        /// An object of the given type with random data assigned to its members.
        /// </returns>
        public static T Generate<T>(this IRandomObjectGenerator generator)
        {
            return (T)generator.Generate(typeof(T), new Attribute[0]);
        }

        /// <summary>
        /// Generate a collection of objects of a given type.
        /// </summary>
        /// <typeparam name="T">The type of the object to create.</typeparam>
        /// <param name="generator">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <param name="count">
        /// A value corresponding to the number of objects to create.
        /// </param>
        /// <returns>
        /// A collection of zero or more objects of the given type
        /// with random data assigned to their members.
        /// </returns>
        public static IEnumerable<T> Generate<T>(this IRandomObjectGenerator generator, int count)
        {
            return generator.Generate<T>(new Attribute[0], count);
        }

        /// <summary>
        /// Generate an object of a given type.
        /// </summary>
        /// <typeparam name="T">The type of the object to generate.</typeparam>
        /// <param name="generator">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <param name="attributes">
        /// Attributes that should be taken into consideration by the current
        /// generator when generating objects.
        /// </param>
        /// <returns>
        /// An object of the given type with random data assigned to its members.
        /// </returns>
        public static T Generate<T>(this IRandomObjectGenerator generator, IEnumerable<Attribute> attributes)
        {
            return (T)generator.Generate(typeof(T), attributes);
        }

        /// <summary>
        /// Generate an object of a given type.
        /// </summary>
        /// <typeparam name="T">The type of the object to generate.</typeparam>
        /// <param name="generator">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <param name="attributes">
        /// Attributes that should be taken into consideration by the current
        /// generator when generating objects.
        /// </param>
        /// <param name="count">
        /// A value corresponding to the number of objects to create.
        /// </param>
        /// <returns>
        /// An object of the given type with random data assigned to its members.
        /// </returns>
        public static IEnumerable<T> Generate<T>(
            this IRandomObjectGenerator generator,
            IEnumerable<Attribute> attributes,
            int count
            )
        {
            for (var i = 0; i < count; i++)
            {
                yield return (T)generator.Generate(typeof(T), attributes);
            }
        }
    }
}