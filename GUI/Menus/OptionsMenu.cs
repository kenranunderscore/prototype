namespace prototype.GUI.Menus
{
    internal class OptionsMenu : Menu
    {
        public OptionsMenu(TextRenderer textRenderer, Options options)
            : base(textRenderer, options)
        {
            MenuItems.Add(new MenuItem("Back") { TargetSceneType = TargetSceneType.MainMenu, IsActive = true });
            CalculateAreas();
        }
    }
}
