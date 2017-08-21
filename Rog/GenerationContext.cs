using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rog
{
    /// <summary>
    /// Represents a collection of value that constitute a context within
    /// which objects with random data can be generated.
    /// </summary>
    public struct GenerationContext : IRandomNumberGenerator, IRandomObjectGenerator
    {
        /// <summary>
        /// Get an empty array of attributes.
        /// </summary>
        public static readonly Attribute[] NoAttributes = new Attribute[0];

        float nullChance;
        IRandomNumberGenerator rng;
        IRandomObjectGenerator rog;

        /// <summary>
        /// Get or set a collection of attributes associated with the current context.
        /// </summary>
        public IEnumerable<Attribute> AssociatedAttributes;

        /// <summary>
        /// Get or set the type associated with the current context.
        /// </summary>
        public Type CurrentType;

        /// <summary>
        /// Get or set the encoding associated with the current context.
        /// </summary>
        public Encoding Encoding;

        /// <summary>
        /// Get or set the maximum length that a sequence generated against the current context can be.
        /// </summary>
        public int MaxSequenceLength;

        /// <summary>
        /// Get or set the maximum length that a string generated against the current context can be.
        /// </summary>
        public int MaxStringLength;

        /// <summary>
        /// Get or set the minimum length that a sequence generated against the current context can be.
        /// </summary>
        public int MinSequenceLength;

        /// <summary>
        /// Get or set the minimum length that a string generated against the current context can be.
        /// </summary>
        public int MinStringLength;

        /// <summary>
        /// Get or set the chance, as a value between 0 and 1, that a value generated against
        /// a nullable type and the current context will be null.
        /// </summary>
        public float NullChance
        {
            get { return nullChance; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException("Null chance must be between 0 and 1.");
                }

                nullChance = value;
            }
        }

        /// <summary>
        /// Create a new instance of the <see cref="GenerationContext"/> structure.
        /// </summary>
        /// <param name="rng">
        /// A random number generation strategy to associate with the new context.
        /// </param>
        /// <param name="rog">
        /// A random object generation strategy to associate with the new context.
        /// </param>
        public GenerationContext(IRandomNumberGenerator rng, IRandomObjectGenerator rog)
        {
            nullChance = 0;

            AssociatedAttributes = NoAttributes;
            CurrentType = null;
            Encoding = null;
            MaxSequenceLength = 0;
            MaxStringLength = 0;
            MinSequenceLength = 0;
            MinStringLength = 0;

            this.rng = rng;
            this.rog = rog;
        }

        /// <summary>
        /// Generate an object with random member data against the current context.
        /// </summary>
        /// <param name="type">The type of the object to generate.</param>
        /// <param name="attributes">
        /// Attributes that should be taken into consideration by the current
        /// context when generating objects.
        /// </param>
        /// <returns>
        /// An object generated against the current context.
        /// </returns>
        public object Generate(Type type, IEnumerable<Attribute> attributes)
        {
            return rog.Generate(type, attributes ?? NoAttributes);
        }

        /// <summary>
        /// Attempt to get an attribute of the specified generic type from the current context.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to get.</typeparam>
        /// <returns>
        /// The given attribute if it exists; null if the current context does not
        /// contain the given attribute.
        /// </returns>
        public T GetAttribute<T>() where T : Attribute
        {
            return AssociatedAttributes.Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
        }

        /// <summary>
        /// Fill the elements of a given array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes to populate with random numbers.
        /// </param>
        public void GetBytes(byte[] buffer)
        {
            rng.GetBytes(buffer);
        }

        /// <summary>
        /// Determine whether the current context has an attribute of the given type
        /// associated with it.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to get.</typeparam>
        /// <returns></returns>
        public bool HasAttribute<T>() where T : Attribute
        {
            return AssociatedAttributes.Select(x => x.GetType()).Any(x => x == typeof(T));
        }

        /// <summary>
        /// Get the next signed 32-bit integer.
        /// </summary>
        /// <param name="minval">
        /// A value corresponding to the minimum value that can be generated.
        /// </param>
        /// <param name="maxval">
        /// A value corresponding to the maximum value that can be generated.
        /// </param>
        /// <returns>
        /// An integer value within the given range.
        /// </returns>
        public int NextInt32(int minval, int maxval)
        {
            return rng.NextInt32(minval, maxval);
        }

        /// <summary>
        /// Get a value corresponding to whether or not a null should be returned.
        /// </summary>
        /// <returns>
        /// True if the roll was succesful; false otherwise.
        /// </returns>
        public bool RollForNull()
        {
            return rng.NextInt32(0, 99) < 100 * nullChance;
        }
    }
}