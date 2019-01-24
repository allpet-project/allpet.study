using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;

namespace blockChain
{

    public class txInfo
    {
        public int value;
        public string ip;
        public string time;
    }
    class Server
    {
        socket.SocketManager _server;
        int blockcount = -1;
        private List<txInfo> msgBox = new List<txInfo>();


        public Server()
        {
            _server = new socket.SocketManager(Config.remoteIP,Config.remoteport);
            Console.WriteLine("server: start watch port：" + Config.remoteIP.ToString() + ":" + Config.remoteport.ToString());

            _server.OnReceiveMsg += (ip, msg) =>
            {
                Console.WriteLine("server receive:" + msg);
                var tx= new txInfo()
                {
                    value = int.Parse(msg.ToString()),
                    ip = ip,
                    time = DateTime.Now.ToString()
                };
                msgBox.Add(tx);
            };

            _server.OnConnected += (remotePort) =>
            {
                Console.WriteLine("server :succes connect to " + remotePort);
            };

            Timer timer = initTimer();
            timer.Elapsed += produceBlock;

            initDiretory();
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
            //Console.WriteLine("produce block. time:" + time.ToString());

            blockcount++;

            var block = new MyJson.JsonNode_Object();
            var txs=new MyJson.JsonNode_Array();
            block.Add("txs",txs);
            block.Add("blockIndex", new MyJson.JsonNode_ValueNumber(blockcount));
            for (int i=0;i<this.msgBox.Count;i++)
            {
                var item = this.msgBox[i];
                var txitem = new MyJson.JsonNode_Object();
                txs.Add(txitem);
                txitem.Add("value",new MyJson.JsonNode_ValueNumber(item.value));
                txitem.Add("time", new MyJson.JsonNode_ValueString(item.time));
                txitem.Add("ip", new MyJson.JsonNode_ValueString(item.ip));
            }
            this.msgBox.Clear();

            string fileName =this.blockcount.ToString()+".txt";
            string filepath= Path.Combine(Config.directorypath, fileName);
            if (!File.Exists(filepath))
            {
                FileStream fs = new FileStream(filepath, FileMode.Create);
                fs.Close();
                fs.Dispose();
            }
            using (StreamWriter writer = new StreamWriter(filepath, false))
            {
                writer.WriteLine(block.ToString());
            }
        }


        void initDiretory()
        {
            var dir = new DirectoryInfo(Config.directorypath);
            if(dir.Exists)
            {
                var files = dir.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    files[i].Delete();
                }
            }else
            {
                dir.Create();
            }

        }
    }
}
