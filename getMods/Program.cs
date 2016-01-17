using System;
using getArgs;

namespace getMods
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "-help" || args[0] == "/help" || args[0] == "-h" || args[0] == "/h")
            {
                getArgs.Program.Help();
            }
            if (args.Length < 1)
                return;
            else if (args[0] == "-backup" || args[0] == "/backup" || args[0] == "-b" || args[0] == "/b")
            {
                if (args.Length < 2)
                return;
                if (args.Length == 3)
                    getArgs.Program.Backup(args[1], args[2]);
                else
                    getArgs.Program.Backup(args[1], null);
            }
            else if (args[0] == "-restore" || args[0] == "/restore" || args[0] == "-r" || args[0] == "/r")
            {
                if (args.Length < 2)
                    return;
                if (args.Length == 3) 
                    getArgs.Program.Restore(args[1], args[2]);
                else
                    getArgs.Program.Restore(args[1], null);
            }
            else if (args[0] == "-patch" || args[0] == "/patch" || args[0] == "-p" || args[0] == "/p")
            {
                if (args.Length < 4)
                    return;
                getArgs.Program.Patch(args[1], args[2], args[3]);
            }
            else if (args[0] == "-zip" || args[0] == "/zip" || args[0] == "-z" || args[0] == "/z")
            {
                if (args.Length < 2)
                    return;
                getArgs.Program.Zip(args[1]);
            }
            else if (args[0] == "-download" || args[0] == "/download" || args[0] == "-d" || args[0] == "/d")
            {
                if (args.Length < 3 && args[2] == "*.zip")
                    return;
                getArgs.Program.Download(args[1], args[2]);
            }
            else
            {
                if (args.Length < 2)
                    return;
                getArgs.Program.List(args[0], args[1]);
            }
        }
    }
}