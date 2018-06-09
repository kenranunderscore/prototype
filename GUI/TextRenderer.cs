namespace prototype.GUI
{
    using System;
    using static SDL2.SDL;

    internal class TextRenderer
    {
        private readonly ITextureLoader textureLoader_;
        private readonly TextCropper cropper_;
        private readonly ILetterClips letterClips_;
        private readonly Options options_;
        private ITexture letters_;

        public TextRenderer(
            ITextureLoader textureLoader,
            TextCropper cropper,
            ILetterClips letterClips,
            Options options)
        {
            textureLoader_ = textureLoader;
            cropper_ = cropper;
            letterClips_ = letterClips;
            options_ = options;
        }

        public void Initialize(IntPtr renderer)
        {
            letters_ = textureLoader_.LoadTexture(
                "Resources/letters.png",
                renderer,
                new SDL_Color { r = 0xff, g = 0, b = 0xdc });
        }

        public void Render(string text, SDL_Rect targetArea)
        {
            //TODO escape sequences and other stuff
            for (var j = 0; j < text.Length; j++)
            {
                var character = text[j];
                letters_.Render(
                    targetArea.x + j * options_.ScaledLetterWidth,
                    targetArea.y,
                    letterClips_.GetClip(character),
                    options_.Scale);
            }
        }

        public void Render(string text, SDL_Rect targetArea, SDL_Color color)
        {
            letters_.ColorMod = color;
            Render(text, targetArea);
        }

        public void RenderCropped(string text, SDL_Rect area)
        {
            Render(cropper_.Crop(text, area.w, options_.ScaledLetterHeight), area);
        }
    }
}
