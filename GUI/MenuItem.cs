namespace prototype.GUI
{
    using static SDL2.SDL;

    internal class MenuItem
    {
        private static readonly SDL_Color NormalColor = new SDL_Color { r = 0xff, g = 0xff, b = 0xff };
        private static readonly SDL_Color MouseOverColor = new SDL_Color { r = 0xff, g = 0xff };
        private readonly TextRenderer textRenderer_;
        private readonly string caption_;
        private readonly SDL_Rect area_;
        private bool isMouseOver_;
        public bool IsActive { get; set; }
        private SDL_Color Color => IsActive || isMouseOver_ ? MouseOverColor : NormalColor;

        public MenuItem(TextRenderer textRenderer, string caption, SDL_Rect area)
        {
            textRenderer_ = textRenderer;
            caption_ = caption;
            area_ = area;
        }

        public void Render()
        {
            textRenderer_.Render(caption_, area_, Color);
        }

        public void HandleEvent(SDL_Event e)
        {
            if (e.type == SDL_EventType.SDL_MOUSEMOTION && !IsActive)
            {
                isMouseOver_ = area_.Contains(e.motion.x, e.motion.y);
            }
        }
    }
}
