using NUnit.Framework;
using Should;
using System;

namespace Rog.Tests
{
    [TestFixture, Category("Unit")]
    public class PercentageTests
    {
        [TestCase]
        public void CanCreatePercentageFromFloat()
        {
            var percentage = new Percentage(0.5f);

            percentage.ToString().ShouldEqual("50%");

            percentage = 0.25f;

            percentage.ToString().ShouldEqual("25%");
        }

        [TestCase]
        public void CanCreatePercentageFromInt()
        {
            var percentage = new Percentage(10);

            percentage.ToString().ShouldEqual("10%");

            percentage = 75;

            percentage.ToString().ShouldEqual("75%");
        }

        [TestCase]
        public void CreatingPercentageWithFloatGreaterThanOneThrowsArgumentException()
        {
            Action action = () => new Percentage(1.5f);

            action.ShouldThrow<ArgumentException>();
        }

        [TestCase]
        public void CreatingPercentageWithFloatLessThanZeroThrowsArgumentException()
        {
            Action action = () => new Percentage(-10.9f);

            action.ShouldThrow<ArgumentException>();
        }

        [TestCase]
        public void CreatingPercentageWithIntegerGreaterThan100ThrowsArgumentException()
        {
            Action action = () => new Percentage(101);

            action.ShouldThrow<ArgumentException>();
        }

        [TestCase]
        public void CreatingPercentageWithIntegerLessThanZeroThrowsArgumentException()
        {
            Action action = () => new Percentage(-50);

            action.ShouldThrow<ArgumentException>();
        }

        [TestCase]
        public void PercentageIsAppliedCorrectly()
        {
            (100 * new Percentage(50)).ShouldEqual(50);
        }

        [TestCase]
        public void SettingAPercentageByFloatGreaterThanOneThrowsArgumentException()
        {
            var test = new Percentage();

            Action action = () => test = 1.1f;

            action.ShouldThrow<ArgumentException>();
        }

        [TestCase]
        public void SettingAPercentageByFloatLessThanZeroThrowsArgumentException()
        {
            var test = new Percentage();

            Action action = () => test = -0.1f;

            action.ShouldThrow<ArgumentException>();
        }

        [TestCase]
        public void SettingAPercentageByIntegerGreaterThan100ThrowsArgumentException()
        {
            var test = new Percentage();

            Action action = () => test = 200;

            action.ShouldThrow<ArgumentException>();
        }

        [TestCase]
        public void SettingAPercentageByIntegerLessThanZeroThrowsArgumentException()
        {
            var test = new Percentage();

            Action action = () => test = -20;

            action.ShouldThrow<ArgumentException>();
        }
    }
}