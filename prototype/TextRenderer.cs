namespace prototype.GUI
{
    using System;
    using System.Collections.Generic;
    using static SDL2.SDL;

    internal class TextRenderer
    {
        //TODO parameterize width/height to enable loading different png dimensions/ratios

        private readonly TextureLoader textureLoader_;
        private TextureWrapper letters_;

        

        public TextRenderer(TextureLoader textureLoader)
        {
            textureLoader_ = textureLoader;
        }

        public void Initialize(IntPtr renderer)
        {
            letters_ = textureLoader_.LoadTexture("Resources/letters.png", renderer);
        }

        public void Render(string text)
        {
            //TODO escape sequences and other stuff
            for (var i = 0; i < text.Length; i++)
            {
                var character = text[i];
                letters_.Render(i * LetterClips.LetterWidth * 2, 0, LetterClips.GetClip(character) , 2);
            }
        }
    }
}
