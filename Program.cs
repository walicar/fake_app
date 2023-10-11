using CommandLine;

namespace Fake
{
    public class Program
    {
        public class Options
        {
            [Value(0, Required = true, MetaName = "count", HelpText = "How many times I will say hello")]
            public int Count {get; set;}
        }
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Run).WithNotParsed(Err);
        }
        public static void Run(Options opt)
        {
            for (int i = 0; i < opt.Count; i++)
            {
                Console.WriteLine($"Hi there! at {i + 1}...");
                Thread.Sleep(3000);
            }
            Console.WriteLine("Done!");
        }

        public static void Err(IEnumerable<Error> errs) {
            Console.WriteLine($"Bad usage: {errs}");
        }
    }
}
