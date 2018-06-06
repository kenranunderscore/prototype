namespace prototype.GUI.Test
{
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class CoreTest
    {
        [Test]
        public void Initialization_is_not_successful_when_any_component_could_not_be_initialized()
        {
            var initializerMock = new Mock<ISdlInitializer>();
            initializerMock.Setup(_ => _.Initialize()).Returns(SdlResult.Invalid("foo"));
            var core = new Core(initializerMock.Object, new Options(800, 600));
            Assert.That(core.Initialize().Success, Is.False);
        }
    }
}
