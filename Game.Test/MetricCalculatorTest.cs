namespace prototype.Game.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class MetricCalculatorTest
    {
        [Test]
        public void Words_per_minute_are_calculated_correctly()
        {
            var calculator = new MetricCalculator();
            const int wordCount = 240;
            const int milliseconds = 120000;
            var wpm = calculator.WordsPerMinute(wordCount, milliseconds);
            Assert.That(wpm, Is.EqualTo(120));
        }

        [Test]
        public void Words_per_minute_are_rounded_correctly()
        {
            var calculator = new MetricCalculator();
            const int wordCount = 148;
            const int milliseconds = 90000;
            var wpm = calculator.WordsPerMinute(wordCount, milliseconds);
            Assert.That(wpm, Is.EqualTo(98));
        }
    }
}
