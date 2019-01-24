using System;

namespace blockChain
{
    class Program
    {
        static void Main(string[] args)
        {


            new Server();
            new client();

            while (true) { };
        }

    }


    public class Config
    {
        public static string remoteIP= "127.0.0.1";
        public static int remoteport=4321;
    }
}
