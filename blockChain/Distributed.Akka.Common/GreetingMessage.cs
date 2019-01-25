using System;

namespace Distributed.Akka.Common
{
    public class GreetingMessage
    {
    }

    public class StartComputeMessage
    {
        public int byteCount;
        public StartComputeMessage(int bytes=0)
        {
            this.byteCount = bytes;
        }
    }

    public class EndComputeMessage
    {
    }

    public class ComputeResultMessage
    {
        public int count;
        public double time;
        public double speed;
    }
}
