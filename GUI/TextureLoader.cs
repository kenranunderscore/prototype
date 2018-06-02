namespace prototype.GUI
{
    using System;
    using System.Runtime.InteropServices;
    using static SDL2.SDL;
    using static SDL2.SDL_image;

    internal class TextureLoader : ITextureLoader
    {
        public ITexture LoadTexture(string path, IntPtr renderer)
        {
            var surface = IMG_Load(path);
            var typedSurface = Marshal.PtrToStructure<SDL_Surface>(surface);
            var texture = SDL_CreateTextureFromSurface(renderer, surface);
            SDL_FreeSurface(surface);
            return new Texture(texture, renderer, typedSurface.w, typedSurface.h);
        }
    }
}
