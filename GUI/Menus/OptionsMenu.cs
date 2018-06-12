namespace prototype.GUI.Menus
{
    using static SDL2.SDL;

    internal class OptionsMenu : Menu
    {
        public OptionsMenu(TextRenderer textRenderer, Options options)
            : base(textRenderer, options)
        {
            MenuItems.Add(new MenuItem("Foo") { TargetSceneType = TargetSceneType.MainMenu, IsActive = true });
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
