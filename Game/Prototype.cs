namespace Game
{
    using System;
    using System.Diagnostics;

    public class Prototype
    {
        private readonly Stopwatch timer_ = new Stopwatch();
        private readonly string fullText_;
        private readonly TextAnalyzer textAnalyzer_;
        private readonly MetricCalculator metricCalculator_;

        public string Text { get; private set; }

        public Prototype(string text, TextAnalyzer textAnalyzer, MetricCalculator metricCalculator)
        {
            fullText_ = text;
            Text = text;
            textAnalyzer_ = textAnalyzer;
            metricCalculator_ = metricCalculator;
        }

        public bool Type(char key)
        {
            if (!timer_.IsRunning)
            {
                timer_.Start();
            }

            if (Text.Length > 0 && Text[0] != key)
            {
                return false;
            }

            Text = Text.Substring(1);
            if (Text.Length == 0)
            {
                timer_.Stop();
            }

            return true;
        }

        public int WordsPerMinute()
        {
            var wordCount = textAnalyzer_.CountWords(fullText_);
            return metricCalculator_.WordsPerMinute(wordCount, timer_.ElapsedMilliseconds);
        }
    }
}
