namespace prototype.GUI
{
    using SDL2;

    public static class ColorScheme
    {
        public static readonly SDL.SDL_Color Default = new SDL.SDL_Color { r = 0xff, g = 0xff, b = 0xff };
        public static readonly SDL.SDL_Color MouseOverItem = new SDL.SDL_Color { g = 0xff };
        public static readonly SDL.SDL_Color ActiveItem = new SDL.SDL_Color { r = 0xff, g = 0xff };
    }
}
