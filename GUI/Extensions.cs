namespace prototype.GUI
{
    using SDL2;

    internal static class Extensions
    {
        public static bool Contains(this SDL.SDL_Rect rect, int x, int y) =>
            x >= rect.x
            && y >= rect.y
            && x <= rect.x + rect.w
            && y <= rect.y + rect.h;
    }
}
