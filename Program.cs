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
                    
                    case "-a":
                    case "-add":
                        Console.WriteLine($"ADD");
                        /*
                        if (arguments.Length == 3)
                            menuAdd(arguments[1], arguments[2]);
                        else if (arguments.Length == 2)
                            menuAdd(arguments[1]);
                        else
                            menuAdd();
                        */
                        break;
                    
                    case "-l":
                    case "-list":
                        Console.WriteLine($"LIST");
                        break;
                    
                    case "-r":
                    case "-remove":
                        Console.WriteLine($"REMOVE");
                        break;

                    case "-v":
                    case "-version":
                        Console.WriteLine($"Current Version: {Global.version}");
                        break;

                    case "-t":
                    case "-test":
                        var list = new List<string> { "test1", "test2", "test3" };
                        var list2 = new List<string> {"Another", "String", "Test"};
                        Focus.AddFocus(list);
                        Focus.AddFocus(list2);
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
            -a,  -add       Add a new Task to the tasklist
            -l,  -list      List all tasks in the tasklist
            -r,  -remove    Remove a task from the tasklist
            -v,  -version   Show the current application version");
        Console.WriteLine(@"Examples:
            focus-tracker -focus ""example_title"" 10
            focus-tracker -");
        Console.WriteLine(@"Description:
            The Focus Session Tracker is a command line tool designed to help you track your focus sessions. 
            By specifying a session title and duration, you can start a timer to keep you on track with your tasks. 
            When the session is complete, you will receive a notification.");
        Console.WriteLine("");
    }

    /*
    private void menuAdd(string focusTitle, string focusDuration)
    {
        Focus newFocus = new Focus(focusTitle, focusDuration);
        newFocus.FocusReport();
    }

    private void menuAdd(string focusTitle)
    {
        Console.WriteLine($"Please specify a duration for this focus.");
        Console.Write("Focus Duration: ");
        string focusDuration = Console.ReadLine();

        Focus newFocus = new Focus(focusTitle, focusDuration);
        newFocus.FocusReport();
    }

    private void menuAdd()
    {
        Console.WriteLine("No parameters passed, please specify a title and a duration.");
        Console.Write("Focus Title: ");
        string focusTitle = Console.ReadLine();

        Console.Write("Focus Duration: ");
        string focusDuration = Console.ReadLine();

        Focus newFocus = new Focus(focusTitle, focusDuration);
        newFocus.FocusReport();
    }

    */
}

static class Focus
{
   static private List<List<string>> focusList = new List<List<string>>();

   static public void AddFocus(List<string> focusDetails)
   {
        focusList.Add(focusDetails);
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
        //args.Cancel = true;
    }

}