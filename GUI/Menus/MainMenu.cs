namespace prototype.GUI.Menus
{
    using static SDL2.SDL;

    internal class MainMenu : Menu
    {
        public MainMenu(TextRenderer textRenderer, Options options)
            : base(textRenderer, options)
        {
            MenuItems.Add(new MenuItem("Play") { TargetSceneType = TargetSceneType.Game, IsActive = true });
            MenuItems.Add(new MenuItem("Options") { TargetSceneType = TargetSceneType.Options });
            MenuItems.Add(new MenuItem("Quit") { TargetSceneType = TargetSceneType.Quit });
            CalculateAreas();
        }

        public override void Render()
        {
            SDL_GetMouseState(out var x, out var y);
            foreach (var menuItem in MenuItems)
            {
                var color = MenuItemColor(menuItem, x, y);
                TextRenderer.Render(menuItem.Caption, menuItem.Area, color);
            }
        }

        private static SDL_Color MenuItemColor(MenuItem menuItem, int x, int y)
        {
            if (menuItem.Area.Contains(x, y))
            {
                return ColorScheme.ActiveItem;
            }

            if (menuItem.IsActive)
            {
                return ColorScheme.MouseOverItem;
            }

            return ColorScheme.Default;
        }
    }
}
