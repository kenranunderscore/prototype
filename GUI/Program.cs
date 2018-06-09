namespace prototype.GUI
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new Options(1280, 720);
            var core = new Core(
                new SdlInitializer(),
                new TextRenderer(new TextureLoader(), new TextCropper(), new LetterClips(), options),
                options);
            var initializationResult = core.Initialize();
            if (!initializationResult.Success)
            {
                Console.WriteLine(initializationResult.Message);
            }

            core.Run();
            core.Cleanup();
        }
    }
}
