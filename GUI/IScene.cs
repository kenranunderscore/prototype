namespace prototype.GUI
{
    using SDL2;

    internal interface IScene
    {
        void Render();
        TargetSceneType HandleEvent(SDL.SDL_Event e);
    }
}
