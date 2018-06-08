namespace prototype.GUI
{
    using SDL2;

    internal class GameScene : IScene
    {
        private readonly TextRenderer textRenderer_;

        public GameScene(TextRenderer textRenderer)
        {
            textRenderer_ = textRenderer;
        }

        public void Render()
        {
            textRenderer_.Render("blub", new SDL.SDL_Rect { x = 100, y = 100 });
        }

        public TargetScene HandleEvent(SDL.SDL_Event e) => TargetScene.Game;
    }
}
