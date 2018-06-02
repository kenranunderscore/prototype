namespace prototype.GUI
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var core = new Core(new SdlInitializer());
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
