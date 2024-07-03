namespace FocusTimer
{
    static class Globals
    {
        public static bool exitSwitch = false;
    }

    class Focus
    {
        public string? Title {get; set;}
        public int Time {get; set;}
        public bool Status = false;
        private int timer = 0;
        

        public void Timer()
        {

            while (timer < Time && !Globals.exitSwitch)
            {
                Console.Write($"\r{timer}");
                Thread.Sleep(5);
                timer++;
            }

            Status = timer == Time ? true: false;
        }

        public void SaveToHistory()
        {
            return;
        }
    }

    class Application
    {

        static void Main(string[] args)
        {
            Focus currentFocus = new Focus();

            if (args.Length == 0)
            {
                Console.Write($"Please enter a Focus Title: ");
                currentFocus.Title = Console.ReadLine();

                Console.Write($"Please enter a Focus Time: ");
                currentFocus.Time = Int32.Parse(Console.ReadLine())*60;
            }
            else
            {
                currentFocus.Title = args[0];
                currentFocus.Time = Int32.Parse(args[1]);
            }

            Console.CancelKeyPress += new ConsoleCancelEventHandler(closeHandler);
            currentFocus.Timer();

            // Test Outputs
            Console.WriteLine("\n---------------------------------");
            Console.WriteLine($"Title: {currentFocus.Title}\nTime: {currentFocus.Time / 60} minutes\nCompleted: {currentFocus.Status}");
        }

        void Reporting(Focus focus)
        {
            return;
        }

        protected static void closeHandler(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("\nFocus Cancelled");
            args.Cancel = true;
            Globals.exitSwitch = true;
        }
    }
}