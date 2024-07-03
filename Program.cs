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
            while (timer <= Time && !Globals.exitSwitch)
            {
                Console.Write($"\r{Title} - {timer}");
                Thread.Sleep(5);
                timer++;
            }

            Status = timer >= Time ? true: false;
            SaveToHistory(Title, Time, Status);
        }

        public void SaveToHistory(string title, int time, bool status)
        {
            DateTime timeStamp = DateTime.Today;
            string completed = status ? "Completed" : "Not Finished";
            string csvEntry = $"\n{timeStamp.ToString("d")}, {title}, {time}, {completed}";
            File.AppendAllText("history.csv", csvEntry);
        }
    }

    class Application
    {
        static Focus currentFocus = new Focus();

        static void Main(string[] args)
        {

            Console.CancelKeyPress += new ConsoleCancelEventHandler(closeHandler);

            if (args.Length != 0 && args[0].StartsWith("-"))
            {
                string menuOption = args[0].ToLower();
                switch (menuOption)
                {
                    case "-help":
                        menuHelp();
                        break;

                    case "-focus":
                        menuFocus(args);
                        currentFocus.Timer();
                        break;

                    default:
                        Console.WriteLine("Invalid Menu Input");
                        break;
                }
            }
            else
                Console.WriteLine("Invalid Option - Type 'dotnet run -help' for further options");

        }

        static void menuFocus(string[] menu)
        {
            if (menu.Length == 3)
            {
                currentFocus.Title = menu[1];
                currentFocus.Time = Int32.Parse(menu[2])*60;
            }
            else
            {
                Console.Write($"Please enter a Focus Title: ");
                currentFocus.Title = Console.ReadLine();

                Console.Write($"Please enter a Focus Time: ");
                currentFocus.Time = Int32.Parse(Console.ReadLine())*60;
            }
        }
        
        static void menuHelp()
        {
            Console.WriteLine($"Focus Session Tracker - Command Line Tool\n");
            Console.WriteLine(@"Usage:
               focus-session [options]");
            Console.WriteLine(@"Options:
               -h,  -help      Show the help page for this application
               -f,  -focus     Start a new focus session
               -v,  -version   Show the current application version");
            Console.WriteLine(@"Examples:
               focus-tracker -focus ""example_title"" 10");
            Console.WriteLine(@"Description:
                The Focus Session Tracker is a command line tool designed to help you track your focus sessions. 
                By specifying a session title and duration, you can start a timer to keep you on track with your tasks. 
                When the session is complete, you will receive a notification.");
            Console.WriteLine("");
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