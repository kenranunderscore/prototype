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

        public void Free()
        {
            SDL_DestroyTexture(texture_);
        }
    }
}
