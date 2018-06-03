namespace prototype.GUI.Test
{
    using NUnit.Framework;
    using SDL2;

    [TestFixture]
    public class ExtensionsTest
    {
        [Test]
        public void Contains_correctly_identifies_points_too_far_left()
        {
            var area = new SDL.SDL_Rect { x = 10, y = 20, w = 10, h = 5 };
            Assert.That(area.Contains(9, 12), Is.False);
        }

        [Test]
        public void Contains_correctly_identifies_points_too_far_right()
        {
            var area = new SDL.SDL_Rect { x = 10, y = 20, w = 10, h = 5 };
            Assert.That(area.Contains(22, 12), Is.False);
        }

        [Test]
        public void Contains_correctly_identifies_points_too_far_at_the_top()
        {
            var area = new SDL.SDL_Rect { x = 10, y = 20, w = 10, h = 5 };
            Assert.That(area.Contains(15, 5), Is.False);
        }

        [Test]
        public void Contains_correctly_identifies_points_too_far_at_the_bottom()
        {
            var area = new SDL.SDL_Rect { x = 10, y = 20, w = 10, h = 5 };
            Assert.That(area.Contains(14, 28), Is.False);
        }

        [Test]
        public void Contains_correctly_identifies_points_inside()
        {
            var area = new SDL.SDL_Rect { x = 10, y = 20, w = 10, h = 5 };
            Assert.That(area.Contains(20, 20));
        }
    }
}
