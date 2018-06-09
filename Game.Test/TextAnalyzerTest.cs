namespace prototype.Game.Test
{
    using global::Game;
    using NUnit.Framework;

    [TestFixture]
    public class TextAnalyzerTest
    {
        [Test]
        public void A_single_word_is_counted_correctly()
        {
            var textAnalyzer = new TextAnalyzer();
            const string text = "SomeLongWord";
            Assert.That(textAnalyzer.CountWords(text), Is.EqualTo(1));
        }
    }
}
