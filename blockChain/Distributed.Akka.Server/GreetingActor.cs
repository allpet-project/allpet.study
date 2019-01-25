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

        private int msg_byte;
        protected override void OnReceive(object message)
        {
            if(message is StartComputeMessage)
            {
                this.recCount = 0;
                this.startTime = DateTime.Now;
                this.byteCount = 0;
                this.msg_byte = (message as StartComputeMessage).byteCount;
            }else if(message is EndComputeMessage)
            {
                this.computeSpeed(Sender);
            }else
            {
                recCount++;
                byteCount += (message as byte[]).Length;
            }
        }

        private void computeSpeed(IActorRef sender)
        {
            TimeSpan time = DateTime.Now - startTime;
            double mbs = byteCount / (1024.0 * 1024.0 * time.Seconds);
            var res = new ComputeResultMessage()
            {
                count = recCount,
                speed = mbs,
                time= time.TotalSeconds
            };
            Console.WriteLine("发Msg次数：{0}  msg大小：{1}kb  时间{2}  速率：{3}m/s   ", this.recCount, msg_byte/1024, time.TotalSeconds, mbs);
            sender.Tell(res);
        }
    }
}
