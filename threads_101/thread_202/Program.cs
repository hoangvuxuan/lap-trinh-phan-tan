using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thread_202
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("enter length of list:  ");
            int N = Convert.ToInt32(Console.ReadLine());

            Console.Write("enter number of input thread:  ");
            int input_thread = Convert.ToInt32(Console.ReadLine());

            Console.Write("enter number of output thread:  ");
            int output_thread = Convert.ToInt32(Console.ReadLine());

            
            thread_semaphore t_s = new thread_semaphore(N, input_thread, output_thread);
            
            t_s.set_value();
            t_s.get_value();
            Console.ReadKey();
        }
    }
}
