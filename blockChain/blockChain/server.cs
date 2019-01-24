using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace blockChain
{
    class Server
    {
        socket.SocketManager _server;

        public Server()
        {
            _server = new socket.SocketManager(Config.remoteIP,Config.remoteport);
            Console.WriteLine("server: start watch port：" + Config.remoteIP.ToString() + ":" + Config.remoteport.ToString());

            _server.OnReceiveMsg += (ip, msg) =>
            {
                Console.WriteLine("server receive:" + msg);
            };

            _server.OnConnected += (remotePort) =>
            {
                Console.WriteLine("server :succes connect to " + remotePort);
            };

            Timer timer = initTimer();
            timer.Elapsed += produceBlock;
        }

        Timer initTimer()
        {
            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.AutoReset = true;
            timer.Enabled = true;
            return timer;
        }

        void produceBlock(object sender, ElapsedEventArgs e)
        {
            var time = DateTime.Now;
            Console.WriteLine("produce block. time:" + time.ToString());
        }
    }
}
