using System;
using System.Collections.Generic;
using System.Linq;

namespace Rog
{
    /// <summary>
    /// Extended functionality for dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        internal static void Add(this Dictionary<Type, ConstructorData> cache, Type type)
        {
            var data = type.GetConstructors()
                           .Select(x => new ConstructorData(x))
                           .OrderByDescending(x => x.ArgTypes.Length)
                           .First();

            cache.Add(type, data);
        }

        /// <summary>
        /// Set the chance that an object of a given type will be null.
        /// </summary>
        /// <param name="chances">The current dictionary of chances.</param>
        /// <param name="type">The type of the null-capable object.</param>
        /// <param name="chance">
        /// The chance, as a single-precision floating point value between 0 and 1,
        /// that an object  of the given type will be null.
        /// </param>
        public static void SetChanceFor(this Dictionary<Type, Percentage> chances, Type type, float chance)
        {
            if (chance == 0f && chances.ContainsKey(type))
            {
                chances.Remove(type);
            }
            else
            {
                chances[type] = chance;
            }
        }

        /// <summary>
        /// Set the chance that an object of a given type will be null.
        /// </summary>
        /// <param name="chances">The current dictionary of chances.</param>
        /// <param name="type">The type of the null-capable object.</param>
        /// <param name="chance">
        /// The chance, as an integer value between 0 and 100,
        /// that an object  of the given type will be null.
        /// </param>
        public static void SetChanceFor(this Dictionary<Type, Percentage> chances, Type type, int chance)
        {
            if (chance == 0f && chances.ContainsKey(type))
            {
                chances.Remove(type);
            }
            else
            {
                chances[type] = chance;
            }
        }

        /// <summary>
        /// Set the chance that an object of a given type will be null.
        /// </summary>
        /// <typeparam name="T">The type of the null-capable object.</typeparam>
        /// <param name="chances">The current dictionary of chances.</param>
        /// <param name="chance">
        /// The chance, as a single-precision floating point value between 0 and 1,
        /// that an object  of the given type will be null.
        /// </param>
        public static void SetChanceFor<T>(this Dictionary<Type, Percentage> chances, float chance)
        {
            chances.SetChanceFor(typeof(T), chance);
        }

        /// <summary>
        /// Set the chance that an object of a given type will be null.
        /// </summary>
        /// <typeparam name="T">The type of the null-capable object.</typeparam>
        /// <param name="chances">The current dictionary of chances.</param>
        /// <param name="chance">
        /// The chance, as an integer value between 0 and 100,
        /// that an object  of the given type will be null.
        /// </param>
        public static void SetChanceFor<T>(this Dictionary<Type, Percentage> chances, int chance)
        {
            chances.SetChanceFor(typeof(T), chance);
        }
    }
}