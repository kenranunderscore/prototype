namespace prototype.GUI.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class SdlResultTest
    {
        [Test]
        public void Valid_creates_a_successful_result()
        {
            var sdlResult = SdlResult.Valid;
            Assert.That(sdlResult.Success);
        }

        [Test]
        public void Valid_creates_a_new_instance_for_each_call()
        {
            var firstResult = SdlResult.Valid;
            var secondResult = SdlResult.Valid;
            Assert.That(firstResult, Is.Not.SameAs(secondResult));
        }

        [Test]
        public void Invalid_creates_a_result_that_is_not_successful()
        {
            var sdlResult = SdlResult.Invalid("foo");
            Assert.That(sdlResult.Success, Is.False);
        }

        [Test]
        public void Message_is_adopted_from_the_factory_method()
        {
            var msg = "foo bar";
            var sdlResult = SdlResult.Invalid(msg);
            Assert.That(sdlResult.Message, Is.EqualTo(msg));
        }
    }
}
