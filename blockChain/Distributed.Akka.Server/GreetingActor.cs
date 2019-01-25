using Akka.Actor;
using Distributed.Akka.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distributed.Akka.Server
{
    class GreetingActor : TypedActor, IHandle<GreetingMessage>
    {
        public void Handle(GreetingMessage message)
        {
            Console.WriteLine("Hello world!");
        }
    }
}
