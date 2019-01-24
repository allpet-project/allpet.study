using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace blockChain
{
    class client
    {
        private socket.SocketClientManager _client;

        public client()
        {
            this._client = new socket.SocketClientManager(Config.remoteIP,Config.remoteport);
            this._client.OnConnected += () =>
            {
                Console.WriteLine("client :succes connect to " + Config.remoteIP.ToString() + ":" + Config.remoteIP.ToString());
            };
            Thread LoopSend = new Thread(RandomSend);
            LoopSend.Start();
        }

        public void sendMessage(string msg)
        {
            Console.WriteLine("client send:" + msg);
            this._client.SendMsg(msg);
        }


        public void RandomSend()
        {
            var random = new Random();
            while (true)
            {
                int number = random.Next(-10, 10);
                this.sendMessage(number.ToString());
                Thread.Sleep(300);
            }

        }
    }
}
