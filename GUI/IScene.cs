namespace prototype.GUI
{
    using SDL2;

    internal interface IScene
    {
        void Render();
        TargetScene HandleEvent(SDL.SDL_Event e);
    }
}
