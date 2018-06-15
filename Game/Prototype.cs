namespace prototype.Game
{
    using System.Diagnostics;

    public class Prototype
    {
        private readonly Stopwatch timer_ = new Stopwatch();
        private readonly TextAnalyzer textAnalyzer_;
        private readonly MetricCalculator metricCalculator_;
        private readonly TextProcessor textProcessor_;
        private string fullText_;
        private string text_;

        public string Text
        {
            get => text_;
            set
            {
                fullText_ = value;
                text_ = value;
            }
        }

        public Prototype(
            TextAnalyzer textAnalyzer,
            MetricCalculator metricCalculator,
            TextProcessor textProcessor)
        {
            textAnalyzer_ = textAnalyzer;
            metricCalculator_ = metricCalculator;
            textProcessor_ = textProcessor;
        }

        public void LoadFile(string path)
        {
            Text = textProcessor_.LoadFile(path);
        }

        public bool Type(char key)
        {
            if (!timer_.IsRunning)
            {
                timer_.Start();
            }

            if (text_.Length > 0 && Text[0] != key)
            {
                return false;
            }

            text_ = text_.Substring(1);
            if (text_.Length == 0)
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
