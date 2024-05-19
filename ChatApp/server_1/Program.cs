using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace server_1
{
    internal class Program
    {
        public static List<int> list_max;
        static void Main(string[] args)
        {
            list_max = new List<int>();
            Console.Title = "Chat Server";
            Console.WriteLine("Starting server...");

            IPAddress localAddress = IPAddress.Parse("127.0.0.1");
            int port = 8080;

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Any, port));
            server.Listen(10);

            Console.WriteLine($"Server started and listening on {localAddress}:{port}");

            while (true)
            {
                Socket client =   server.Accept();
                Console.WriteLine("New client connected!");

                Task task = Task.Run(() => { HandleClient(client); });
            }
        }

        private static void HandleClient(object state)
        {
            Socket client = (Socket)state;
            byte[] buffer = new byte[1024];
            int bytesRead;

            while (client.Connected)
            {
                try
                {
                    bytesRead = client.Receive(buffer);
                    if (bytesRead > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Received message: {message}");

                        string max = get_max(message).ToString();
                        byte[] response = Encoding.ASCII.GetBytes(max);
                        byte[] response2 = Encoding.ASCII.GetBytes(message);
                        //client.Send(response2);

                        //client.Send(response);
                        string s = string.Join(" ", list_max);
                        byte[] response3 = Encoding.ASCII.GetBytes(s);
                        //Console.WriteLine("hhghg " + s);
                        client.Send(response3);
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Socket error: {ex.Message}");
                    break;
                }
            }

            client.Shutdown(SocketShutdown.Both);
            client.Close();
            Console.WriteLine("Client disconnected.");
        }

        private static int get_max(string s)
        {
            var arr = s.Split(' ');
            List<int> list = new List<int>();

            foreach (string a in arr)
            {
                list.Add(Int32.Parse(a));
            }
            Semaphore semaphore = new Semaphore(1, 1);

            semaphore.WaitOne();
            list_max.Add(list.Max());
            foreach(var a in list_max)
            {
                Console.WriteLine(a);
            }
            semaphore.Release();
            return list.Max();
        }
    }
    
}
