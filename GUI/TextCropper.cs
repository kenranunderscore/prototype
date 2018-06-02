namespace prototype.GUI
{
    internal class TextCropper
    {
        public string Crop(string text, int targetAreaWidth, int letterWidth)
        {
            var maximumNumberOfLetters = targetAreaWidth / letterWidth;
            return maximumNumberOfLetters < text.Length
                ? text.Substring(0, maximumNumberOfLetters)
                : text;
        }
    }
}
