namespace prototype.GUI
{
    using Game;
    using SDL2;

    internal class GameScene : IScene
    {
        private readonly TextRenderer textRenderer_;
        private readonly Prototype prototype_;

        public GameScene(Prototype prototype, TextRenderer textRenderer)
        {
            textRenderer_ = textRenderer;
            prototype_ = prototype;
        }

        public void Render()
        {
            textRenderer_.Render(prototype_.Text, new SDL.SDL_Rect { x = 100, y = 100 });
        }

        public TargetScene HandleEvent(SDL.SDL_Event e) => TargetScene.Game;
    }
}
