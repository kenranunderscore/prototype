namespace prototype.GUI
{
    using Game;
    using static SDL2.SDL;

    internal class GameScene : IScene
    {
        private readonly TextRenderer textRenderer_;
        private readonly Prototype prototype_;
        private readonly Options options_;
        private static readonly SDL_Color ErrorColor = new SDL_Color { r = 0xff };
        private static readonly SDL_Color DefaultColor = new SDL_Color { r = 0xff, g = 0xff, b = 0xff };
        private SDL_Color currentColor_ = DefaultColor;

        public GameScene(Prototype prototype, TextRenderer textRenderer, Options options)
        {
            textRenderer_ = textRenderer;
            prototype_ = prototype;
            options_ = options;
        }

        public void Render()
        {
            var targetArea = CalculateRenderArea(options_.ScreenWidth, options_.ScreenHeight);
            textRenderer_.Render(prototype_.Text, targetArea, currentColor_);
        }

        public TargetSceneType HandleEvent(SDL_Event e)
        {
            if (e.type == SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL_Keycode.SDLK_ESCAPE)
            {
                return TargetSceneType.MainMenu;
            }

            if (e.type == SDL_EventType.SDL_TEXTINPUT)
            {
                var character = SdlInputHelper.GetPressedCharacter(e.text);
                currentColor_ = prototype_.Type(character) ? DefaultColor : ErrorColor;
            }

            return TargetSceneType.Unchanged;
        }

        //TODO get Defaults.LetterHeight out of here.
        // perhaps give text renderer an option to render vertically centered within an area
        private static SDL_Rect CalculateRenderArea(int width, int height) =>
            new SDL_Rect
            {
                x = (int)(0.3 * width),
                y = (int)(0.5 * height - 0.5 * Defaults.LetterHeight),
                w = (int)(0.5 * width)
            };
    }
}
