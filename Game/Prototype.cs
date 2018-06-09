namespace Game
{
    public class Prototype
    {
        public string Text { get; private set; }

        public Prototype(string text)
        {
            Text = text;
        }

        public bool Type(char key)
        {
            if (Text[0] != key)
            {
                return false;
            }

            Text = Text.Substring(1);
            return true;
        }
    }
}
