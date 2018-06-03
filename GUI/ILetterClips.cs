namespace prototype.GUI
{
    using SDL2;

    internal interface ILetterClips
    {
        SDL.SDL_Rect GetClip(char c);
    }
}