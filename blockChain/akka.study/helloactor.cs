using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace akka.study
{
    class Helloactor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Console.WriteLine("hello akka.net!");
        }
    }
}
