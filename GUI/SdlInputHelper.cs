namespace prototype.GUI
{
    using SDL2;

    public static class SdlInputHelper
    {
        public static char GetPressedCharacter(SDL.SDL_TextInputEvent textInputEvent)
        {
            unsafe
            {
                var text = Utf8Helper.GetUtf8String(textInputEvent.text);
                return text[0]; // assume only small characters are actually used
            }
        }
    }
}
