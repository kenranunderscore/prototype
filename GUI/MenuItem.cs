namespace prototype.GUI
{
    using SDL2;

    internal class MenuItem
    {
        private readonly TextRenderer textRenderer_;
        private readonly string caption_;
        private readonly SDL.SDL_Rect area_;

        public MenuItem(TextRenderer textRenderer, string caption, SDL.SDL_Rect area)
        {
            textRenderer_ = textRenderer;
            caption_ = caption;
            area_ = area;
        }

        public void Render()
        {
            textRenderer_.Render(caption_, area_);
        }
    }
}
