using Akka.Actor;
using Akka.Configuration;
using Distributed.Akka.Common;
using System;

namespace DistributedAkkaClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
            akka {  
                actor {
                    provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                }
                remote {
                    helios.tcp {
                        transport-class = ""Akka.Remote.Transport.Helios.HeliosTcpTransport, Akka.Remote""
                        applied-adapters = []
                        transport-protocol = tcp
                        port = 0
                        hostname = localhost
                    }
                }
            }
            ");

            using (var system = ActorSystem.Create("MyClient", config))
            {
                while(true)
                {
                    Console.WriteLine("cmd:");
                    var cmd = Console.ReadLine();
                    switch(cmd)
                    {
                        case "1":
                            IActorRef testtps = system.ActorOf(Props.Create(() => new TestTPS()));
                            testtps.Tell(new StartComputeMessage());
                            break;
                        case "2":
                            break;
                    }
                }
            }
        }
    }
}
