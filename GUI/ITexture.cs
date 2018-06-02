namespace prototype.GUI
{
    using SDL2;

    internal interface ITexture
    {
        void Render(int x, int y, double scale = 1d);
        void Render(int x, int y, SDL.SDL_Rect clip, double scale = 1d);
        void SetColor(SDL.SDL_Color color);
        void Free();
    }
}