namespace prototype.Game
{
    using System;
    using System.IO;

    public class TextProcessor
    {
        public string LoadFile(string path)
        {
            var fileContent = File.ReadAllText(path);
            return ProcessText(fileContent);
        }

        private static string ProcessText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException(nameof(text));
            }

            var processed = new char[text.Length];
            var index = 0;
            var hasSkipped = false;
            foreach (var c in text)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (!hasSkipped)
                    {
                        if (index > 0)
                        {
                            processed[index++] = ' ';
                        }

                        hasSkipped = true;
                    }
                }
                else
                {
                    hasSkipped = false;
                    processed[index++] = c;
                }
            }

            return new string(processed, 0, index);
        }
    }
}
