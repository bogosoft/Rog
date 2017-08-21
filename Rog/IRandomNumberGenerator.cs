namespace Rog
{
    /// <summary>
    /// Provides a means of generating random numbers.
    /// </summary>
    public interface IRandomNumberGenerator
    {
        /// <summary>
        /// Fill the elements of a given array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes to populate with random numbers.
        /// </param>
        void GetBytes(byte[] buffer);

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
        int NextInt32(int minval, int maxval);
    }
}