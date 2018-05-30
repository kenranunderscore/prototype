namespace prototype.GUI
{
    using static SDL2.SDL;
    using static SDL2.SDL_image;

    internal class SdlInitializer : ISdlInitializer
    {
        public SdlResult Initialize()
        {
            SDL_SetHint(SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
            if (SDL_Init(SDL_INIT_VIDEO) < 0 || IMG_Init(IMG_InitFlags.IMG_INIT_PNG) < 0)
            {
                return SdlResult.Invalid($"Failed to initialize SDL or any of its parts: {SDL_GetError()}");
            }

            return SdlResult.Valid;
        }
    }
}
