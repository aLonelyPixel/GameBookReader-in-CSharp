using System;
using System.Threading;

namespace GameBook.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("-------Hello and welcome to the best Gamebook application!-------\n");
            ITerminal myTerminal = new ITerminal();

            myTerminal.Loop();
        }
    }
}
