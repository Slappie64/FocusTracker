static class Global
{
    static public string version = "v0.01";
}

class UserInterface
{
    public void MainMenu(string[] arguments)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                arguments[i] = arguments[i].ToLower();
            }

            if (arguments.Length != 0 && arguments[0].StartsWith("-"))
            {
                switch (arguments[0])
                {
                    case "-h":
                    case "-help":
                        MenuHelp();
                        break;
                    
                    case "-f":
                    case "-focus":
                        if (arguments.Length != 3)
                            Focus.FocusStart();
                        else
                            Focus.FocusStart(arguments[1], arguments[2]);
                        break;

                    case "-v":
                    case "-version":
                        Console.WriteLine($"Current Version: {Global.version}");
                        break;

                    case "-t":
                    case "-test":
                        Focus.Report();
                        break;                    
                    default:
                        MenuHelp();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Invalid or Missing Arguments");
            }
        }

    private void MenuHelp()
    {
        Console.WriteLine($"Focus Session Tracker - Command Line Tool\n");
        Console.WriteLine(@"Usage:
            focus-session [options]");
        Console.WriteLine(@"Options:
            -h,  -help      Show the help page for this application
            -f,  -focus     Start a new focus session
            -v,  -version   Show the current application version");
        Console.WriteLine(@"Examples:
            focus-tracker -focus ""example_title"" 10
            focus-tracker -version");
        Console.WriteLine(@"Description:
            The Focus Session Tracker is a command line tool designed to help you track your focus sessions. 
            By specifying a session title and duration, you can start a timer to keep you on track with your tasks. 
            When the session is complete, you will receive a notification.");
        Console.WriteLine("");
    }
}

static class Focus
{

    static public List<string> loadingIcons = new List<string> {"█     ", "██    ", "███   ", "████  ", "█████ ", "██████", " █████", "  ████", "   ███", "    ██","     █", "      "};
    static private List<List<string>> focusList = new List<List<string>>();

    static public void FocusStart()
    {
        Console.Write($"Focus Title: ");
        string? focusTitle = Console.ReadLine();

        Console.Write($"How long do you need to focus for?: ");
        string? focusDuration = Console.ReadLine();

        if (focusDuration is null)
        {
            Console.WriteLine($"Invalid Duration.");
            FocusStart();
        }
        else
        {
            FocusStart(focusTitle, focusDuration);
        }
    }
    static public void FocusStart(string focusTitle, string focusDuration)
    {
        Console.Clear();
        Console.WriteLine($"Focus: {focusTitle}");
        int timer = 0;
        int totalDuration = Int32.Parse(focusDuration)*60;

        do
        {
            foreach (string icon in loadingIcons)
            {
                Console.Write($"\r{icon}{icon}{icon}");
                Thread.Sleep(1000);
                timer++;
            }
        } while (timer != totalDuration);
    }

    static public void Report()
    {
            foreach (List<string> focus in focusList)
            {
                Console.WriteLine($"{focus[0]} {focus[1]} {focus[2]}");
            }
    }
}

static class Application
{

    static void Main(string[] args)
    {
        Console.CancelKeyPress += new ConsoleCancelEventHandler(closeHandler);
        UserInterface ui = new UserInterface();

        ui.MainMenu(args);
    }

    static void closeHandler(object sender, ConsoleCancelEventArgs args)
    {
        Console.WriteLine("\nFocus Cancelled");
        //args.Cancel = true;    }
    }
}