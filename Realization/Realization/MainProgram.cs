using System;
using System.IO;
using System.Collections.Generic;

namespace Logger
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, it is my logger realization");
            string path = "H:/Bars/Logger/DebugInformation" + "/" + DateTime.Now.ToShortDateString();
            ILog logger = new Logger(path);

            Random rand = new Random();
            List<Exception> exceptions = new List<Exception>();
            exceptions.Add(new IndexOutOfRangeException());
            exceptions.Add(new StackOverflowException());
            exceptions.Add(new FieldAccessException());
            exceptions.Add(new FileLoadException());
            exceptions.Add(new FileNotFoundException());
            exceptions.Add(new Exception());

            for(int i = 0; i < 100; i++)
            {
                if (rand.Next(0, 2) == 0)
                {
                    logger.Error(exceptions[rand.Next(0, exceptions.Count)]);
                }
                else
                {
                    logger.Error("no, it is error", exceptions[rand.Next(0, exceptions.Count)]);
                }
            }

            Console.ReadKey();
        }
    }
}
