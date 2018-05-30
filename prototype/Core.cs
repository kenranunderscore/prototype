namespace prototype.GUI
{
    using System;
    using static SDL2.SDL;

    internal class Core
    {
        private IntPtr window_;
        private IntPtr renderer_;
        private readonly ISdlInitializer initializer_;

        public Core(ISdlInitializer initializer)
        {
            initializer_ = initializer;
        }

        public SdlResult Initialize()
        {
            var initializationResult = initializer_.Initialize();
            if (!initializationResult.Success)
            {
                return initializationResult;
            }

            window_ = SDL_CreateWindow(
                "prototype",
                SDL_WINDOWPOS_UNDEFINED,
                SDL_WINDOWPOS_UNDEFINED,
                800,
                600,
                SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if (window_ == IntPtr.Zero)
            {
                return SdlResult.Invalid("Window creation failed");
            }

            renderer_ = SDL_CreateRenderer(window_, 0, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            return SdlResult.Valid;
        }

        public void Run()
        {
            while (true)
            {
                if (SDL_PollEvent(out var evnt) != 0)
                {
                    if (evnt.type == SDL_EventType.SDL_QUIT
                        || evnt.type == SDL_EventType.SDL_KEYDOWN
                        && evnt.key.keysym.sym == SDL_Keycode.SDLK_ESCAPE)
                    {
                        break;
                    }
                }

                SDL_SetRenderDrawColor(renderer_, 0xff, 0xff, 0xff, 0xff);
                SDL_RenderClear(renderer_);
                SDL_RenderPresent(renderer_);
            }
        }

        public void Cleanup()
        {
            SDL_DestroyRenderer(renderer_);
            SDL_DestroyWindow(window_);
            SDL_Quit();
        }
    }
}
