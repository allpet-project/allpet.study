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
            if (message is byte[])
            {
                if (recCount == 0)
                {
                    startTime = DateTime.Now;
                    //Console.WriteLine(startTime.ToString());
                }
                recCount++;
                byteCount += (message as byte[]).Length;
                if (recCount % 20000 == 0)
                {
                    Console.WriteLine("msgcount: " + recCount + "    byteCount:" + byteCount + "   time:" + (DateTime.Now - startTime).ToString());
                }
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
