using Akka.Actor;
using Distributed.Akka.Common;
using System;

namespace DistributedAkkaClient
{
    public class TestTPS : UntypedActor
    {

        public TestTPS()
        {

        }

        private int msg_byte = 1024;
        private int testIndex = 1;
        protected override void OnReceive(object message)
        {
            if(message is StartComputeMessage)
            {
                var greeting = Context.ActorSelection("akka.tcp://MyServer@localhost:8081/user/Greeting");
                Console.WriteLine("通信测试{0}：",testIndex);
                greeting.Tell(new StartComputeMessage(this.msg_byte));
                for (int i = 0; i < 100000; i++)
                {
                    greeting.Tell(new byte[msg_byte]);
                }
                greeting.Tell(new EndComputeMessage());
            }
            else if(message is ComputeResultMessage)
            {
                var msg = message as ComputeResultMessage;
                testIndex++;
                if(testIndex<=10)
                {
                    this.msg_byte += 2048;
                    Self.Tell(new StartComputeMessage(0));
                }

            }
        }
    }
}
