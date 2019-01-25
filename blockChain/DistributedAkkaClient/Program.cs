﻿using Akka.Actor;
using Akka.Configuration;
using Distributed.Akka.Common;
using System;

namespace DistributedAkkaClient
{
    class Program
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
                var greeting = system.ActorSelection("akka.tcp://MyServer@localhost:8081/user/Greeting");

                while (true)
                {
                    var input = Console.ReadLine();
                    if (input.Equals("b"))
                    {
                        for(int i=0;i<100000;i++)
                        {
                            greeting.Tell(new byte[1024]);
                        }
                    }
                    else
                    {
                        greeting.Tell(input);
                    }
                }
            }
        }
    }
}