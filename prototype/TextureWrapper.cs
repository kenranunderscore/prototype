namespace prototype.GUI
{
    using System;
    using System.Runtime.InteropServices;
    using static SDL2.SDL;
    using static SDL2.SDL_image;

    internal class TextureWrapper
    {
        private readonly IntPtr texture_;
        private readonly IntPtr renderer_;
        private readonly int width_;
        private readonly int height_;

        private TextureWrapper(IntPtr texture, IntPtr renderer, int width, int height)
        {
            texture_ = texture;
            renderer_ = renderer;
            width_ = width;
            height_ = height;
        }

        public static TextureWrapper FromFile(string path, IntPtr renderer)
        {
            var surface = IMG_Load(path);
            var typedSurface = Marshal.PtrToStructure<SDL_Surface>(surface);
            var texture = SDL_CreateTextureFromSurface(renderer, surface);
            SDL_FreeSurface(surface);
            return new TextureWrapper(texture, renderer, typedSurface.w, typedSurface.h);
        }

        public void Free()
        {
            SDL_DestroyTexture(texture_);
        }
    }
}
