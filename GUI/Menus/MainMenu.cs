namespace prototype.GUI.Menus
{
    internal class MainMenu : Menu
    {
        public MainMenu(TextRenderer textRenderer, Options options)
            : base(textRenderer, options)
        {
            MenuItems.Add(new MenuItem("Play") { TargetSceneType = TargetSceneType.FileChoice, IsActive = true });
            MenuItems.Add(new MenuItem("Options") { TargetSceneType = TargetSceneType.Options });
            MenuItems.Add(new MenuItem("Quit") { TargetSceneType = TargetSceneType.Quit });
            CalculateAreas();
        }
    }
}
