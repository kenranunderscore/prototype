namespace Game
{
    using System;

    public class MetricCalculator
    {
        public int WordsPerMinute(int wordCount, long milliseconds)
        {
            return (int)Math.Floor(60000 * wordCount / (decimal)milliseconds);
        }
    }
}
