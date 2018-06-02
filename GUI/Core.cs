namespace prototype.GUI
{
    using System;
    using static SDL2.SDL;

    internal class Core
    {
        private readonly ISdlInitializer initializer_;
        private readonly Screen screen_;
        private IntPtr window_;
        private IntPtr renderer_;

        public Core(ISdlInitializer initializer, Screen screen)
        {
            initializer_ = initializer;
            screen_ = screen;
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
                screen_.Width,
                screen_.Height,
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
            var textRenderer = new TextRenderer(new TextureLoader(), new TextCropper(), new LetterClips());
            textRenderer.Initialize(renderer_);
            var menu = new MainMenu(textRenderer, screen_);

            while (true)
            {
                if (SDL_PollEvent(out var e) != 0)
                {
                    if (e.type == SDL_EventType.SDL_QUIT
                        || e.type == SDL_EventType.SDL_KEYDOWN
                        && e.key.keysym.sym == SDL_Keycode.SDLK_ESCAPE)
                    {
                        break;
                    }
                }

                SDL_SetRenderDrawColor(renderer_, 0, 0, 0, 0xff);
                SDL_RenderClear(renderer_);
                menu.Render();
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
