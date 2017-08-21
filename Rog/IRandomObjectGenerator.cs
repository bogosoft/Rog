using System;
using System.Collections.Generic;

namespace Rog
{
    /// <summary>
    /// Provides a means of generating objects with random data assinged to its members.
    /// </summary>
    public interface IRandomObjectGenerator
    {
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
        object Generate(Type type, IEnumerable<Attribute> attributes);
    }
}