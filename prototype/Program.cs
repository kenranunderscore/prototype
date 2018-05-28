namespace prototype.GUI
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var core = new Core();
            if (!core.Initialize())
            {
                Console.WriteLine("Core SDL functionality failed to initialize");
                Environment.Exit(-1);
            }

            core.Run();
            core.Cleanup();
        }
    }
}
