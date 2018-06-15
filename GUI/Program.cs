namespace prototype.GUI
{
    using System;
    using Game;

    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new Options(1280, 720);
            var textRenderer = new TextRenderer(new TextureLoader(), new TextCropper(), new LetterClips(), options);
            var prototype = new Prototype(new TextAnalyzer(), new MetricCalculator(), new TextProcessor());
            var core = new Core(
                new SdlInitializer(),
                textRenderer,
                options,
                prototype,
                new GameScene(
                    prototype,
                    textRenderer,
                    options));
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
