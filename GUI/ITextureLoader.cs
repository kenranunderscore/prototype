namespace prototype.GUI
{
    using System;
    using SDL2;

    internal interface ITextureLoader
    {
        ITexture LoadTexture(string path, IntPtr renderer, SDL.SDL_Color? colorKey = null);
    }
}
