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
            var server = new socket.SocketManager(Config.remoteIP, Config.remoteport);

            var client = new socket.SocketClientManager(Config.remoteIP, Config.remoteport);

            server.OnReceiveMsg += (ip,msg) =>
            {
                Console.WriteLine("server receive:" + msg);
            };

            client.OnConnected += () =>
            {
                client.SendMsg("hello!");
            };

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


    public class Config
    {
        public static string clientIP = "127.0.0.1";
        public static int clientport=1234;

        public static string remoteIP= "127.0.0.1";
        public static int remoteport=4321;
    }
}
