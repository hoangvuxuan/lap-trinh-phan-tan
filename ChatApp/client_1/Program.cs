using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace client_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Chat Client";

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));

            while (true)
            {
                Console.Write("Enter your message: ");
                string message = Console.ReadLine();

                List<int> list = create_list(10);
                string string_list = string.Join(" ", list);

                byte[] data = Encoding.ASCII.GetBytes(string_list);
                client.Send(data);

                data = new byte[1024];
                int bytesRead = client.Receive(data);
                string response = Encoding.ASCII.GetString(data, 0, bytesRead);

                Console.WriteLine($"Server response: {response}");
            }
        }

        private static List<int> create_list(int N)
        {
            Random random = new Random();
            List<int> list = new List<int>();
            for (int i = 0; i < N; i++)
            {
                list.Add(random.Next(1000));
            }

            return list;
        }
    }
}
