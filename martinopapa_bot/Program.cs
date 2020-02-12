using System;
using System.Net;
using System.Net.Http;

namespace martinopapa_bot
{
    class Program
    {
        static void Main(string[] args)
        {         
            Console.WriteLine("in avvio...");
            bot bot = new bot();
            bot.Start();
            Console.WriteLine("attendo messaggi...\n");
            Console.ReadLine();
        }
    }
}
