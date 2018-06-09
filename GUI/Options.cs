namespace prototype.GUI
{
    internal class Options
    {
        public int ScreenWidth { get; }
        public int ScreenHeight { get; }
        public double Scale { get; set; } = 1.8d;
        public int ScaledLetterWidth => (int)(Scale * Defaults.LetterWidth);
        public int ScaledLetterHeight => (int)(Scale * Defaults.LetterHeight);

        public Options(int screenWidth, int screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }
    }
}
