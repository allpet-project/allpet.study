using Akka.Actor;
using Distributed.Akka.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distributed.Akka.Server
{
    class GreetingActor : UntypedActor
    {
        private int recCount = 0;
        private DateTime startTime;
        private int byteCount = 0;
        //public void Handle(object message)
        //{

        //}

        protected override void OnReceive(object message)
        {
            if (recCount == 0)
            {
                startTime = DateTime.Now;
            }
            recCount++;
            byteCount += (message as byte[]).Length;
            if (recCount % 2000 == 0)
            {
                Console.WriteLine("msgcount: " + recCount + "    byteCount:" + byteCount + "   time:" + (DateTime.Now - startTime).ToString());
            }
        }
    }
}
