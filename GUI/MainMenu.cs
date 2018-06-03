namespace prototype.GUI
{
    using System.Collections.Generic;
    using System.Linq;
    using SDL2;

    internal class MainMenu
    {
        private IReadOnlyList<MenuItem> menuItems_;
        private readonly IReadOnlyCollection<string> menuItemCaptions_ = new List<string>
        {
            "Play",
            "Options",
            "Quit"
        };

        public MainMenu(TextRenderer textRenderer, Options options)
        {
            CreateMenuItems(textRenderer, options);
        }

        private void CreateMenuItems(TextRenderer textRenderer, Options options)
        {
            //TODO refactor
            var maxMenuItemWidth = menuItemCaptions_.Max(c => c.Length) * Defaults.LetterWidth;
            var x = options.ScreenWidth / 2 - maxMenuItemWidth / Defaults.LetterWidth;
            var centerY = options.ScreenHeight / 2;
            var menuItemHeight = Defaults.LetterHeight;
            var totalMenuHeight = (2 * menuItemCaptions_.Count - 1) * menuItemHeight;
            var y = centerY - totalMenuHeight / 2;
            menuItems_ = menuItemCaptions_.Select((caption, i) => new MenuItem(
                    textRenderer,
                    caption,
                    new SDL.SDL_Rect
                    {
                        x = x,
                        y = y + 2 * i * menuItemHeight,
                        w = caption.Length * Defaults.LetterWidth,
                        h = Defaults.LetterHeight
                    }))
                .ToList();
        }

        public void Render()
        {
            foreach (var menuItem in menuItems_)
            {
                menuItem.Render();
            }
        }
    }
}
