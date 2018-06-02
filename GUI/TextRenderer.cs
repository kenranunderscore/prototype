﻿namespace prototype.GUI
{
    using System;
    using static SDL2.SDL;

    internal class TextRenderer
    {
        private readonly ITextureLoader textureLoader_;
        private readonly TextCropper cropper_;
        private readonly ILetterClips letterClips_;
        private ITexture letters_;

        public TextRenderer(ITextureLoader textureLoader, TextCropper cropper, ILetterClips letterClips)
        {
            textureLoader_ = textureLoader;
            cropper_ = cropper;
            letterClips_ = letterClips;
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
                    targetArea.x + j * letterClips_.LetterWidth,
                    targetArea.y,
                    letterClips_.GetClip(character));
            }
        }

        public void Render(string text, SDL_Rect targetArea, SDL_Color color)
        {
            //TODO escape sequences and other stuff
            letters_.SetColor(color);
            Render(text, targetArea);
        }

        public void RenderCropped(string text, SDL_Rect area)
        {
            Render(cropper_.Crop(text, area.w, letterClips_.LetterWidth), area);
        }
    }
}
