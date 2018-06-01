namespace prototype.GUI
{
    using System;
    using static SDL2.SDL;

    internal class TextureWrapper
    {
        private readonly IntPtr texture_;
        private readonly IntPtr renderer_;
        private readonly int width_;
        private readonly int height_;

        public TextureWrapper(IntPtr texture, IntPtr renderer, int width, int height)
        {
            texture_ = texture;
            renderer_ = renderer;
            width_ = width;
            height_ = height;
        }

        public void Render(int x, int y, double scale = 1d)
        {
            var renderArea = new SDL_Rect { x = x, y = y, w = Scale(width_, scale), h = Scale(width_, scale) };
            SDL_RenderCopy(renderer_, texture_, IntPtr.Zero, ref renderArea);
        }

        private static int Scale(int length, double scale) => (int)(scale * length);

        public void Free()
        {
            SDL_DestroyTexture(texture_);
        }
    }
}
