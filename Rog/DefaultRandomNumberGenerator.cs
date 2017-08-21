using System;

namespace Rog
{
    /// <summary>
    /// An implementation of the <see cref="IRandomNumberGenerator"/> contract
    /// that utilizes the <see cref="Random"/> class.
    /// </summary>
    public sealed class DefaultRandomNumberGenerator : IRandomNumberGenerator
    {
        Random rng = new Random();

        /// <summary>
        /// Fill the elements of a given array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes to populate with random numbers.
        /// </param>
        public void GetBytes(byte[] buffer)
        {
            rng.NextBytes(buffer);
        }

        /// <summary>
        /// Get the next signed 32-bit integer.
        /// </summary>
        /// <param name="minval">The minimum value that can be generated.</param>
        /// <param name="maxval">The maximum value that can be generated.</param>
        /// <returns>
        /// An integer value within the given range.
        /// </returns>
        public int NextInt32(int minval, int maxval)
        {
            return rng.Next(minval, maxval);
        }
    }
}