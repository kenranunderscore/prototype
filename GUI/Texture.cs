namespace prototype.GUI
{
    using System;
    using static SDL2.SDL;

    internal class Texture : ITexture
    {
        private readonly IntPtr texture_;
        private readonly IntPtr renderer_;
        private readonly int width_;
        private readonly int height_;

        public Texture(IntPtr texture, IntPtr renderer, int width, int height)
        {
            //TODO throw exception when texture or renderer are NULL/IntPtr.Zero
            texture_ = texture;
            renderer_ = renderer;
            width_ = width;
            height_ = height;
        }

        public SDL_Color ColorMod
        {
            set => SDL_SetTextureColorMod(texture_, value.r, value.g, value.b);
        }

        public void Render(int x, int y, double scale = 1d)
        {
            var renderArea = new SDL_Rect { x = x, y = y, w = Scale(width_, scale), h = Scale(height_, scale) };
            SDL_RenderCopy(renderer_, texture_, IntPtr.Zero, ref renderArea);
        }

        public void Render(int x, int y, SDL_Rect clip, double scale = 1d)
        {
            var renderArea = new SDL_Rect { x = x, y = y, w = Scale(clip.w, scale), h = Scale(clip.h, scale) };
            SDL_RenderCopy(renderer_, texture_, ref clip, ref renderArea);
        }

        private static int Scale(int length, double scale) => (int)(scale * length);

        public void Free()
        {
            SDL_DestroyTexture(texture_);
        }
    }
}
