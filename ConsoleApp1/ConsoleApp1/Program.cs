using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace ConsoleApp1
{
    public class Thread_Semaphore
    {
        public Semaphore semaphore;
        public int countR, countPQ;
        int[] numbers = { 100, 500, 1000, 1500 };
        Random random = new Random();
        int sleep = 1000;

        public void Run()
        {
            semaphore = new Semaphore(1, 3);
            countR = countPQ = 0;



            Task threadP = Task.Run(() =>
            {
                while (true)
                {
                    DisplayP();

                    Thread.Sleep(1000);

                }
            });

            Task threadQ = Task.Run(() =>
            {
                while (true)
                {
                    DisplayQ();
                    Thread.Sleep(1000);

                }
            });


            Task threadR = Task.Run(() =>
            {
                while (true)
                {
                    if (countR < countPQ)
                    {
                        DisplayR();
                        Thread.Sleep(1000);

                    }

                }
            });


        }

        public void DisplayP()
        {
            semaphore.WaitOne();
            Console.WriteLine($"P -- P+Q: {++countPQ} R: {countR}");

            int random_index = random.Next(0, numbers.Length);
            sleep = numbers[random_index];
            //Console.WriteLine(sleep);

            semaphore.Release();

        }

        public void DisplayQ()
        {
            semaphore.WaitOne();
            Console.WriteLine($"Q -- P+Q: {++countPQ} R: {countR} ");

            int random_index = random.Next(0, numbers.Length);
            sleep = numbers[random_index];
            // Console.WriteLine(sleep);

            semaphore.Release();

        }

        public void DisplayR()
        {
            semaphore.WaitOne();
            Console.WriteLine($"R -- P+Q: {countPQ} R: {++countR} ");

            int random_index = random.Next(0, numbers.Length);
            sleep = numbers[random_index];
            //Console.WriteLine(sleep);

            semaphore.Release();

        }



        public static void Main(string[] args)
        {

            Thread_Semaphore semaphore = new Thread_Semaphore();
            semaphore.Run();
            Console.ReadKey();

        }
    }

         


}
