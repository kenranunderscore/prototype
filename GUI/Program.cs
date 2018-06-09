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
            var core = new Core(
                new SdlInitializer(),
                textRenderer,
                options,
                new GameScene(
                    new Prototype("Some start text, here.", new TextAnalyzer(), new MetricCalculator()),
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
