using System;

namespace blockChain
{
    class Program
    {
        static bool bechooseActor = false;
        static void Main(string[] args)
        {
            while (true&&bechooseActor==false) {
                Console.Clear();
                Console.WriteLine("选择启动Actor(1=server,2=client,3=syncClient):");
                var actor = Console.ReadLine();
                switch(actor)
                {
                    case "1":
                        new Server();
                        bechooseActor = true;
                        break;
                    case "2":
                        new client();
                        bechooseActor = true;
                        break;
                    case "3":
                        new SyncClient();
                        bechooseActor = true;
                        break;
                }
            };
            while (true) { };
        }
    }


    public class Config
    {
        public static string remoteIP="127.0.0.1";
        public static int remoteport=4321;

        public static string directorypath = "BlockData";

    }
}
