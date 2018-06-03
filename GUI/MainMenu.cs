namespace prototype.GUI
{
    using System.Collections.Generic;
    using System.Linq;
    using SDL2;

    internal class MainMenu
    {
        private readonly IReadOnlyList<string> menuItemCaptions_ = new List<string>
        {
            "Play",
            "Options",
            "Quit"
        };
        private readonly IReadOnlyList<MenuItem> menuItems_;

        public MainMenu(TextRenderer textRenderer, Options options)
        {
            var maxMenuItemWidth = menuItemCaptions_.Max(c => c.Length) * 8; //TODO get letter width here
            var x = options.ScreenWidth / 2 - maxMenuItemWidth / 2; //TODO get letter height here and calculate y correctly
            var y = options.ScreenHeight / 2;
            menuItems_ = menuItemCaptions_.Select((caption, i) => new MenuItem(
                textRenderer,
                caption,
                new SDL.SDL_Rect { x = x, y = y + i * 20 }))
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
