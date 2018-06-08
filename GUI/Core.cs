namespace prototype.GUI
{
    using System;
    using static SDL2.SDL;

    internal class Core
    {
        private readonly ISdlInitializer initializer_;
        private readonly TextRenderer textRenderer_;
        private readonly Options options_;
        private IntPtr window_;
        private IntPtr renderer_;
        private IScene activeScene_;

        public Core(ISdlInitializer initializer, TextRenderer textRenderer, Options options)
        {
            initializer_ = initializer;
            textRenderer_ = textRenderer;
            options_ = options;
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
                options_.ScreenWidth,
                options_.ScreenHeight,
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
            textRenderer_.Initialize(renderer_);
            activeScene_ = new MainMenu(textRenderer_, options_);
            var gameScene = new GameScene(textRenderer_);

            while (true)
            {
                if (SDL_WaitEvent(out var e) != 0)
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
                var targetScene = activeScene_.HandleEvent(e);
                if (targetScene == TargetScene.Quit)
                {
                    break;
                }
                else if (targetScene == TargetScene.Game)
                {
                    activeScene_ = gameScene;
                }

                activeScene_.Render();
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
