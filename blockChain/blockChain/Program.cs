using System;
using System.Timers;

namespace blockChain
{
    class Program
    {
        static Timer timer;
        static void Main(string[] args)
        {
            timer = initTimer();
            timer.Elapsed += produceBlock;

            while (true) { };
        }
        static Timer initTimer()
        {
            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.AutoReset = true;
            timer.Enabled = true;
            return timer;
        }

        static void produceBlock(object sender, ElapsedEventArgs e)
        {
            var time = DateTime.Now;
            Console.WriteLine("produce block. time:"+time.ToString());
        }


    }
}
