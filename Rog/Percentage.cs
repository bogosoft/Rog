using System;

namespace Rog
{
    /// <summary>
    /// Represents a percentage value.
    /// </summary>
    public struct Percentage
    {
        /// <summary>
        /// Take a percentage of a given value.
        /// </summary>
        /// <param name="percentage">The left-hand side of the expression.</param>
        /// <param name="value">The right-hand side of the expression.</param>
        /// <returns>
        /// The result of taking a percentage of the given value.
        /// </returns>
        public static float operator *(Percentage percentage, int value)
        {
            return value * percentage.value;
        }

        /// <summary>
        /// Take a percentage of a given value.
        /// </summary>
        /// <param name="value">The right-hand side of the expression.</param>
        /// <param name="percentage">The left-hand side of the expression.</param>
        /// <returns>
        /// The result of taking a percentage of the given value.
        /// </returns>
        public static float operator *(int value, Percentage percentage)
        {
            return value * percentage.value;
        }

        /// <summary>
        /// Convert a given single-precision floating point value to a percentage.
        /// </summary>
        /// <param name="value">A single-precision floating point value.</param>
        /// <exception cref="ArgumentException">
        /// Thrown in the event that the given float is less than 0 or greater than 1.
        /// </exception>
        public static implicit operator Percentage(float value)
        {
            if (value < 0 || value > 1)
            {
                throw new ArgumentException("Float values must be between 0 and 1.");
            }

            return new Percentage(value);
        }

        /// <summary>
        /// Convert a given integer to a percentage.
        /// </summary>
        /// <param name="value">An integer.</param>
        /// <exception cref="ArgumentException">
        /// Thrown in the event that the given integer is less than 0 or greater than 100.
        /// </exception>
        public static implicit operator Percentage(int value)
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Integer values must be between 0 and 100.");
            }

            return new Percentage(value);
        }

        float value;

        /// <summary>
        /// Create a new percentage from a given value.
        /// </summary>
        /// <param name="value">A single-precision floating point value.</param>
        /// <exception cref="ArgumentException">
        /// Thrown in the event that the given float is less than 0 or greater than 1.
        /// </exception>
        public Percentage(float value)
        {
            if (value < 0 || value > 1)
            {
                throw new ArgumentException("Float values must be between 0 and 1.");
            }

            this.value = value;
        }

        /// <summary>
        /// Create a new percentage from a given value.
        /// </summary>
        /// <param name="value">An integer value.</param>
        /// <exception cref="ArgumentException">
        /// Thrown in the event that the given integer is less than 0 or greater than 100.
        /// </exception>
        public Percentage(int value)
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Integer values must be between 0 and 100.");
            }

            this.value = (float)value / 100;
        }

        /// <summary>
        /// Convert the current percentage to its string representation.
        /// </summary>
        /// <returns>The string representation of the current value.</returns>
        public override string ToString()
        {
            return (value * 100).ToString() + "%";
        }
    }
}