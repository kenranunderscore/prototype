namespace Game.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class PrototypeTest
    {
        [Test]
        public void Text_is_taken_from_constructor_value()
        {
            const string text = "Some test text.";
            var prototype = new Prototype(text);
            Assert.That(prototype.Text, Is.EqualTo(text));
        }
    }
}
