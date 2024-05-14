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
            
            thread_semaphore t_s = new thread_semaphore(100, 5, 7);
            
            t_s.set_value();
            t_s.get_value();
            Console.ReadKey();
        }
    }
}
