namespace prototype.GUI
{
    using System;
    using System.Runtime.InteropServices;
    using static SDL2.SDL;
    using static SDL2.SDL_image;

    internal class TextureLoader : ITextureLoader
    {
        public ITexture LoadTexture(string path, IntPtr renderer, SDL_Color? colorKey = null)
        {
            var surface = IMG_Load(path);
            var typedSurface = Marshal.PtrToStructure<SDL_Surface>(surface);
            if (colorKey.HasValue)
            {
                SDL_SetColorKey(surface, 1, SDL_MapRGB(
                    typedSurface.format,
                    colorKey.Value.r,
                    colorKey.Value.g,
                    colorKey.Value.b));
            }

            var texture = SDL_CreateTextureFromSurface(renderer, surface);
            SDL_FreeSurface(surface);
            return new Texture(texture, renderer, typedSurface.w, typedSurface.h);
        }
    }
}
