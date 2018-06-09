namespace prototype.Game.Test
{
    using global::Game;
    using NUnit.Framework;

    [TestFixture]
    public class PrototypeTest
    {
        [Test]
        public void Text_is_taken_from_constructor_value()
        {
            const string text = "Some test text.";
            var prototype = new Prototype(text, new TextAnalyzer(), new MetricCalculator());
            Assert.That(prototype.Text, Is.EqualTo(text));
        }

        [Test]
        public void Typing_the_correct_key_shortens_the_text_accordingly()
        {
            const string text = "Foo";
            var prototype = new Prototype(text, new TextAnalyzer(), new MetricCalculator());
            prototype.Type('F');
            Assert.That(prototype.Text, Is.EqualTo("oo"));
        }

        [Test]
        public void Typing_is_successful_when_correct_key_is_sent()
        {
            const string text = " F";
            var prototype = new Prototype(text, new TextAnalyzer(), new MetricCalculator());
            var success = prototype.Type(' ');
            Assert.That(success, Is.True);
        }

        [Test]
        public void Typing_is_not_successful_when_wrong_letter_is_sent()
        {
            const string text = "Check";
            var prototype = new Prototype(text, new TextAnalyzer(), new MetricCalculator());
            var success = prototype.Type(' ');
            Assert.That(success, Is.False);
        }

        [Test]
        public void Text_does_not_change_when_wrong_letter_is_sent()
        {
            const string text = "  ";
            var prototype = new Prototype(text, new TextAnalyzer(), new MetricCalculator());
            prototype.Type('/');
            Assert.That(prototype.Text, Is.EqualTo(text));
        }

        [Test]
        public void Typing_is_case_sensitive()
        {
            const string text = "Check";
            var prototype = new Prototype(text, new TextAnalyzer(), new MetricCalculator());
            var success = prototype.Type('c');
            Assert.That(success, Is.False);
        }

        [Test]
        public void After_typing_the_last_correct_key_the_text_is_empty()
        {
            const string text = "1* ";
            var prototype = new Prototype(text, new TextAnalyzer(), new MetricCalculator());
            prototype.Type('1');
            prototype.Type('*');
            prototype.Type(' ');
            Assert.That(prototype.Text, Is.Null.Or.Empty);
        }
    }
}
