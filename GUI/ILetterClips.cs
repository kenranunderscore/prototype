namespace prototype.GUI
{
    using SDL2;

    internal interface ILetterClips
    {
        int LetterWidth { get; }
        int LetterHeight { get; }
        SDL.SDL_Rect GetClip(char c);
    }
}