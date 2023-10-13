﻿using CommandLine;
using System.Runtime.InteropServices;
namespace Fake
{
    public class Program
    {
        static bool exitSystem = false;


        # if WINDOWS
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        
        private delegate bool EventHandler(CtrlType sig);

        static EventHandler? _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            Console.WriteLine("Exiting system due to external CTRL-C, or process kill, or shutdown");
            //do your cleanup here
            Thread.Sleep(5000); //simulate some cleanup delay
            Console.WriteLine("Cleanup complete");
            exitSystem = true;
            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);
            return true;
        }
        #endif

        public class Options
        {
            [Value(0, Required = true, MetaName = "count", HelpText = "How many times I will say hello")]
            public int Count { get; set; }
        }
        public static void Main(string[] args)
        {
            #if WINDOWS
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);
            #endif
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                Program p = new Program();
                p.Run(o);
            }).WithNotParsed(Err);

            while(!exitSystem) {
                #if !WINDOWS
                exitSystem = true; // just a hack for now, normally you would have a handler here.
                #endif
                Thread.Sleep(500);
            }
        }
        public void Run(Options opt)
        {
            Thread work = new(() =>
            {
                for (int i = 0; i < opt.Count; i++)
                {
                    Console.WriteLine($"Hi there! at {i + 1}...");
                    Thread.Sleep(3000);
                }
            });
            work.Start();
            work.Join();
            Console.WriteLine("Done!");
        }

        public static void Err(IEnumerable<Error> errs)
        {
            Console.WriteLine($"Bad usage: {errs}");
        }
    }
}
