using System;
using UniSpy.Server.Core.Logging;

namespace UniSpy.Server.CDKey.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new ServerLauncher().Start();
                Console.WriteLine("Press < Q > to exit. ");
                while (Console.ReadKey().Key != ConsoleKey.Q) { }
            }
            catch (Exception e)
            {
                LogWriter.LogError(e);
            }


        }
    }
}
