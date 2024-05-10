using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threads_101
{
    internal class Program
    {
        static List<int> max_list = new List<int>();
        static void Main(string[] args)
        {
            
            Random random = new Random();
            List<int> list = new List<int>();
            int num_threads;
            int num_list;

            
            Console.Write("enter number of threads: ");
            num_threads = Convert.ToInt32(Console.ReadLine());

            Console.Write("enter number of list: ");
            num_list = Convert.ToInt32(Console.ReadLine());
            

            if(num_list / num_threads < 1)
            {
                return;
            }

            for (int i = 0; i < num_list; i++)
            {
                 list.Add(random.Next(1, 1000));
            }


            create_thread(list, num_threads);
    
        }
        public static List<List<int>> return_child_list(List<int> list, int num_threads)
        {
            int N = list.Count;
            double child = N / num_threads;
            int child_size = Convert.ToInt32(Math.Ceiling(child));
            List<List<int>> list_child_list = new List<List<int>>();
             
            for (int i = 0; i < num_threads; i++)
            {

                if (i == num_threads - 1)
                {
                    list_child_list.Add(list.GetRange(i * child_size, N - i * child_size));
                }
                else
                {
                    list_child_list.Add(list.GetRange(i * child_size, child_size));
                }
            }

            return list_child_list;
        }


        public static void create_thread(List<int> list, int num_threads)
        {
            List<List<int>> child_list = return_child_list(list, num_threads);

            
            foreach (List<int> child in child_list)
            {       
                 
                Thread thread = new Thread(() =>
                {                  
                    find_max(child_list, child);
                });
                thread.Start();
                
            }

        }



        public static void find_max(List<List<int>> child_list, List<int> list)
        {
            
            int max = list.Max();
            int i = child_list.IndexOf(list);

            Console.WriteLine($"T{i}: {max} - {DateTime.Now}");

            max_list.Add(max);
            if (max_list.Count == child_list.Count)
            {
                Console.WriteLine($"final result: {max_list.Max()}");
            }
            //Console.WriteLine(max + "---" + child_list.IndexOf(list));

        }




    }
}
