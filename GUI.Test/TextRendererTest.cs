namespace prototype.GUI.Test
{
    using System;
    using Moq;
    using NUnit.Framework;
    using SDL2;

    [TestFixture]
    public class TextRendererTest
    {
        [Test]
        public void All_characters_are_rendered_when_rendering_normally()
        {
            var textureMock = new Mock<ITexture>();
            var mockedTextureLoader = Mock.Of<ITextureLoader>(
                _ => _.LoadTexture(It.IsAny<string>(), It.IsAny<IntPtr>()) == textureMock.Object);
            var textRenderer = new TextRenderer(mockedTextureLoader, new TextCropper(), new LetterClips());
            textRenderer.Initialize(IntPtr.Zero);
            textRenderer.Render("Foo", new SDL.SDL_Rect { x = 1, y = 2, w = 3, h = 4 });

            textureMock.Verify(
                t => t.Render(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SDL.SDL_Rect>(), 1d),
                Times.Exactly(3));
        }

        [Test]
        public void The_correct_clips_are_taken_from_the_resources()
        {
            var letterClipsMock = new Mock<ILetterClips>();
            var textRenderer = new TextRenderer(new TextureLoader(), new TextCropper(), letterClipsMock.Object);
            textRenderer.Initialize(IntPtr.Zero);
            textRenderer.Render("Foo! ", new SDL.SDL_Rect { x = 1, y = 2, w = 3, h = 4 });

            letterClipsMock.Verify(l => l.GetClip('F'), Times.Once);
            letterClipsMock.Verify(l => l.GetClip('o'), Times.Exactly(2));
            letterClipsMock.Verify(l => l.GetClip('!'), Times.Once);
            letterClipsMock.Verify(l => l.GetClip(' '), Times.Once);
        }
    }
}
