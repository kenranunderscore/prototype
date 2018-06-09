﻿namespace prototype.GUI.Test
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
            var options = new Options(800, 600);
            var core = new Core(
                initializerMock.Object,
                new TextRenderer(new TextureLoader(), new TextCropper(), new LetterClips(), options), 
                options);
            Assert.That(core.Initialize().Success, Is.False);
        }
    }
}
