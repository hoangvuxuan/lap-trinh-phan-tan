using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread_202
{
    internal class thread_semaphore
    {
        private List<int> f_list;
        private int N;
        private int thread_set;
        private int thread_get;
        private Semaphore semaphore;
        private Random random;

        public thread_semaphore(int n, int thread_set, int thread_get)
        {
            this.f_list = new List<int>();
            this.semaphore = new Semaphore(1, 1);
            this.random = new Random();
            N = n;
            this.thread_set = thread_set;
            this.thread_get = thread_get;

        }

        public void set_value()
        {
            for(int i = 0; i < thread_set; i++)
            {
                int name = i;
                Task task = Task.Run(() =>
                {
                    
                    while(true)
                    {
                        try
                        {
                            semaphore.WaitOne(); 
                            if(f_list.Count > N)
                            {
                                semaphore.Release();
                                continue;
                            }     
                            int temp = random.Next(1, 1000);
                            f_list.Add(temp);
                            Console.WriteLine($"W{name}: {temp} - {DateTime.Now} ");
                            semaphore.Release();
                            Thread.Sleep(random.Next(500, 1500));
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                       
                    }  
                    
                }); 

                 
                
            }
        }

        public void get_value()
        {
            for (int i = 0; i < thread_get; i++)
            {
                int name = i;
                Task task = Task.Run(() =>
                {

                    while (true)
                    {
                        try
                        {
                            semaphore.WaitOne();
                            if (f_list.Count == 0)
                            {
                                semaphore.Release();
                                continue;
                            }
                            int r_index = random.Next(0, f_list.Count - 1);
                            int a = f_list[r_index];
                            f_list.Remove(a);
                            string mess = a % 2 == 0 ? $"R{name}: {a} - is even -  {DateTime.Now} " : $"R{name}: {a} - is odd -  {DateTime.Now}";
                            Console.WriteLine(mess);

                            semaphore.Release();
                            Thread.Sleep(random.Next(1000, 2000));

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }

                });



            }
        }






    }
}
