using GOL.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace GOL
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ConsoleKey key;
            SimulatorConsoleTicker ticker = null;
            Console.Clear();
            Console.WriteLine("**********************  Welcome to Conway's Game of Life  **********");
            Console.WriteLine("**********************  Instructions  ******************************");
            Console.WriteLine("********************************************************************");
            Console.WriteLine("Press C for common patterns");
            Console.WriteLine("Press M for manual entry of initial seed");
            Console.WriteLine("Press anyother key to exit");
            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.C) 
            {
                Console.Clear();
                Console.WriteLine("**********************  Choose from following patterns *************");
                var options = Enum.GetNames(typeof(CommonPattern));
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine(string.Format("Press {0} for {1}",i.ToString(),options[i]));
                }
                Console.WriteLine("Press anyother key to exit");
                var keyChar = Console.ReadKey(true).KeyChar;
                int intKey = -1;
                int.TryParse(keyChar.ToString(), out intKey);
                if (intKey > -1 && intKey < options.Length)
                {
                    CommonPattern selectedPattern = (CommonPattern)Enum.Parse(typeof(CommonPattern), options[intKey], true);
                    ticker = new SimulatorConsoleTicker(selectedPattern, 1000);
                }
                else 
                    return;
                
            }
            else if (key == ConsoleKey.M)
            {
                Console.Clear();
                Console.WriteLine("Enter manual feed as 1,1|2,3|4,5");
                string userInput = Console.ReadLine();
                ticker = new SimulatorConsoleTicker(userInput,1000);
            }
            else 
            {
                return;
            }

            Console.WriteLine("Press S to start, any other key to exit now or later");
            key = Console.ReadKey().Key;
            if (key == ConsoleKey.S) 
            {                
                Thread thread = new Thread(ticker.Start);
                thread.Start();

                Console.ReadKey();
                thread.Abort();
            }
            
        }


        
    }
}
