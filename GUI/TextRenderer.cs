namespace prototype.GUI
{
    using System;
    using static SDL2.SDL;

    internal class TextRenderer
    {
        //TODO parameterize width/height to enable loading different png dimensions/ratios

        private readonly TextureLoader textureLoader_;
        private readonly TextCropper cropper_;
        private TextureWrapper letters_;

        public TextRenderer(TextureLoader textureLoader, TextCropper cropper)
        {
            textureLoader_ = textureLoader;
            cropper_ = cropper;
        }

        public void Initialize(IntPtr renderer)
        {
            letters_ = textureLoader_.LoadTexture("Resources/letters.png", renderer);
        }

        public void Render(string text, SDL_Rect targetArea)
        {
            //TODO escape sequences and other stuff
            for (var j = 0; j < text.Length; j++)
            {
                var character = text[j];
                letters_.Render(
                    targetArea.x + j * LetterClips.LetterWidth,
                    targetArea.y,
                    LetterClips.GetClip(character));
            }
        }

        public void RenderCropped(string text, SDL_Rect area)
        {
            Render(cropper_.Crop(text, area.w, LetterClips.LetterWidth), area);
        }
    }
}
