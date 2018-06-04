namespace prototype.GUI
{
    using static SDL2.SDL;

    internal class MenuItem
    {
        private static readonly SDL_Color NormalColor = new SDL_Color { r = 0xff, g = 0xff, b = 0xff };
        private static readonly SDL_Color MouseOverColor = new SDL_Color { r = 0xff, g = 0xff };
        private readonly TextRenderer textRenderer_;
        private readonly string caption_;
        private bool isMouseOver_;
        public SDL_Rect Area { get; }
        public bool IsActive { get; set; }
        public TargetScene TargetScene { get; }
        private SDL_Color Color => IsActive || isMouseOver_ ? MouseOverColor : NormalColor;

        public MenuItem(TextRenderer textRenderer, string caption, SDL_Rect area, TargetScene targetScene)
        {
            textRenderer_ = textRenderer;
            caption_ = caption;
            Area = area;
            TargetScene = targetScene;
        }

        public void Render()
        {
            textRenderer_.Render(caption_, Area, Color);
        }

        public void HandleMouseMotion(SDL_MouseMotionEvent e)
        {
            if (!IsActive)
            {
                isMouseOver_ = Area.Contains(e.x, e.y);
            }
        }
    }
}
