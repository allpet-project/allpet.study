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
            this._client.OnFaildConnect += () =>
            {
                Console.WriteLine("client :reconnect to " + Config.remoteIP.ToString() + ":" + Config.remoteIP.ToString());
                this._client.reconnet();
            };
            Thread LoopSend = new Thread(RandomSend);
            LoopSend.Start();

            while(true)
            {
                Console.WriteLine("client 指令（1=停/发交易，2=账户总额)：");
                var cmd= Console.ReadLine();
                switch(cmd)
                {
                    case "1":
                        this.beActive = !this.beActive;
                        break;
                    case "2":
                        Console.WriteLine("账户总额："+testValue);
                        break;
                }
            }
        }

        public void sendMessage(string msg)
        {
            this._client.SendMsg(msg);
        }

        public int testValue = 0;
        bool beActive = true;
        public void RandomSend()
        {
            var random = new Random();
            while(true)
            {
                if(this._client._isConnected && this.beActive)
                {
                    int number = random.Next(-10, 10);
                    this.sendMessage(number.ToString());
                    testValue += number;
                    Thread.Sleep(300);
                }
            }
        }
    }
}
