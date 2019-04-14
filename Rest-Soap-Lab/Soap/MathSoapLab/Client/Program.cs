using Client.ClientService;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MathsOperationsClient client = new MathsOperationsClient();
            Console.WriteLine(client.Add(100, 101));
            Console.WriteLine(client.Multiply(100, 101));
            Console.ReadLine();
        }
    }
}
