using System;

namespace Rog
{
    /// <summary>
    /// Extended functionality for the <see cref="IRandomNumberGenerator"/> contract.
    /// </summary>
    public static class RandomNumberGeneratorExtensions
    {
        /// <summary>
        /// Get a random boolean value.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random boolean value.</returns>
        public static bool NextBoolean(this IRandomNumberGenerator rng)
        {
            return rng.NextInt32() % 2 == 0;
        }

        /// <summary>
        /// Get a random byte.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random byte.</returns>
        public static byte NextByte(this IRandomNumberGenerator rng)
        {
            return (byte)rng.NextInt32(0, 255);
        }

        /// <summary>
        /// Get a random char.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random char.</returns>
        public static char NextChar(this IRandomNumberGenerator rng)
        {
            var buffer = new byte[2];

            rng.GetBytes(buffer);

            return BitConverter.ToChar(buffer, 0);
        }

        /// <summary>
        /// Get a random date-time.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random date-time.</returns>
        public static DateTime NextDateTime(this IRandomNumberGenerator rng)
        {
            var year = rng.NextInt32(1, 9999);
            var month = rng.NextInt32(1, 12);

            return new DateTime(
                year,
                month,
                DateTime.DaysInMonth(year, month),
                rng.NextInt32(0, 23),
                rng.NextInt32(0, 59),
                rng.NextInt32(0, 59),
                rng.NextInt32(0, 999)
                );
        }

        /// <summary>
        /// Get a random date-time with offset.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A date-time with offset.</returns>
        public static DateTimeOffset NextDateTimeOffset(this IRandomNumberGenerator rng)
        {
            var year = rng.NextInt32(1, 9999);
            var month = rng.NextInt32(1, 12);

            return new DateTimeOffset(
                year,
                month,
                DateTime.DaysInMonth(year, month),
                rng.NextInt32(0, 23),
                rng.NextInt32(0, 59),
                rng.NextInt32(0, 59),
                rng.NextInt32(0, 999),
                new TimeSpan(rng.NextInt32(-14, 14), rng.NextInt32(0, 59), 0)
                );
        }

        /// <summary>
        /// Get a random decimal value.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random decimal value.</returns>
        public static decimal NextDecimal(this IRandomNumberGenerator rng)
        {
            return new decimal(
                rng.NextInt32(),
                rng.NextInt32(),
                rng.NextInt32(),
                rng.NextInt32() % 2 == 0,
                (byte)rng.NextInt32(0, 28)
                );
        }

        /// <summary>
        /// Get a random double precision floating point value.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random double precision floating point value.</returns>
        public static double NextDouble(this IRandomNumberGenerator rng)
        {
            var buffer = new byte[8];

            rng.GetBytes(buffer);

            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Get a random single precision floating point value.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random single precision floating point value.</returns>
        public static float NextFloat(this IRandomNumberGenerator rng)
        {
            return rng.NextInt32();
        }

        /// <summary>
        /// Get a random 16-bit signed integer.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random 16-bit signed integer.</returns>
        public static short NextInt16(this IRandomNumberGenerator rng)
        {
            return (short)rng.NextInt32(-32768, 32767);
        }

        /// <summary>
        /// Get a random 32-bit signed integer.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random 32-bit signed integer.</returns>
        public static int NextInt32(this IRandomNumberGenerator rng)
        {
            return rng.NextInt32(int.MinValue, int.MaxValue);
        }

        /// <summary>
        /// Get a random 32-bit signed integer.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <param name="maxval">
        /// The maximum value that the generated integer can have.
        /// </param>
        /// <returns>A random 32-bit signed integer.</returns>
        public static int NextInt32(this IRandomNumberGenerator rng, int maxval)
        {
            return rng.NextInt32(int.MinValue, maxval);
        }

        /// <summary>
        /// Get a random 64-bit signed integer.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random 64-bit signed integer.</returns>
        public static long NextInt64(this IRandomNumberGenerator rng)
        {
            var buffer = new byte[8];

            rng.GetBytes(buffer);

            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Get a random 8-bit signed integer.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random 8-bit signed integer.</returns>
        public static sbyte NextSByte(this IRandomNumberGenerator rng)
        {
            return (sbyte)rng.NextInt32(-128, 127);
        }

        /// <summary>
        /// Get a random timespan.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random timespan.</returns>
        public static TimeSpan NextTimeSpan(this IRandomNumberGenerator rng)
        {
            return new TimeSpan(rng.NextInt64());
        }

        /// <summary>
        /// Get a random 16-bit unsigned integer.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random 16-bit unsigned integer.</returns>
        public static ushort NextUInt16(this IRandomNumberGenerator rng)
        {
            return (ushort)rng.NextInt32(0, 65535);
        }

        /// <summary>
        /// Get a random 32-bit unsigned integer.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random 32-bit unsigned integer.</returns>
        public static uint NextUInt32(this IRandomNumberGenerator rng)
        {
            var buffer = new byte[4];

            rng.GetBytes(buffer);

            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Get a random 64-bit unsigned integer.
        /// </summary>
        /// <param name="rng">
        /// The current <see cref="IRandomNumberGenerator"/> implementation.
        /// </param>
        /// <returns>A random 64-bit unsigned integer.</returns>
        public static ulong NextUInt64(this IRandomNumberGenerator rng)
        {
            var buffer = new byte[8];

            rng.GetBytes(buffer);

            return BitConverter.ToUInt64(buffer, 0);
        }
    }
}