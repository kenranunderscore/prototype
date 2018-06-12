namespace prototype.GUI
{
    using SDL2;

    internal class MenuItem
    {
        public MenuItem(string caption)
        {
            Caption = caption;
        }

        public string Caption { get; }
        public TargetSceneType TargetSceneType { get; set; }
        public SDL.SDL_Rect Area { get; set; }
        public bool IsActive { get; set; }
    }
}
