using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace blockChain
{

    public class accountInfo
    {
        public string ip;
        public int value=0;
        public List<int> txInfo = new List<int>();
        public accountInfo(string ip)
        {
            this.ip = ip;
        }
    }
    class SyncClient
    {
        socket.SocketClientManager _client;
        Dictionary<string, accountInfo> countInfo = new Dictionary<string, accountInfo>();
        private int blockcount=-1;
        public SyncClient()
        {
            Thread SyncBlock = new Thread(SyncBlockData);
            SyncBlock.Start();
            Thread showAccount = new Thread(showAccountInfo);
            showAccount.Start();
        }
        public void SyncBlockData()
        {
            while(true)
            {
                int nextBlock = blockcount + 1;
                var blockPath = Path.Combine(Config.directorypath, nextBlock + ".txt");
                if (File.Exists(blockPath))
                {
                    string blockdata = File.ReadAllText(blockPath);
                    if(blockdata!=string.Empty)
                    {
                        this.paraseBlockData(blockdata);
                        this.blockcount++;
                    }
                }
                Thread.Sleep(100);
            }
        }

        void paraseBlockData(string data)
        {
            MyJson.JsonNode_Object json = MyJson.Parse(data) as MyJson.JsonNode_Object;
            var txs = json["txs"] as MyJson.JsonNode_Array;
            for(int i=0;i<txs.Count;i++)
            {
                var tx = txs[i] as MyJson.JsonNode_Object;
                int value = tx["value"].AsInt();
                string ip = tx["ip"].AsString();
                if(!this.countInfo.ContainsKey(ip))
                {
                    this.countInfo.Add(ip,new blockChain.accountInfo(ip));
                }
                this.countInfo[ip].value += value;
                this.countInfo[ip].txInfo.Add(value);
            }
        }

        void showAccountInfo()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("账户信息：");
                foreach(var account in this.countInfo)
                {
                    Console.WriteLine("account:"+account.Key+" money:"+account.Value.value);
                }
                Thread.Sleep(500);
            }
        }
    }
}
