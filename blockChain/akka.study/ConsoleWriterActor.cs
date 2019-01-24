using Akka.Actor;
using System;

namespace akka.study
{
    internal class ConsoleWriterActor:UntypedActor
    {
        public ConsoleWriterActor()
        {

        }

        protected override void OnReceive(object message)
        {
            if (message is Messages.InputError)
            {
                var msg = message as Messages.InputError;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(msg.Reason);
            }
            else if (message is Messages.InputSuccess)
            {
                var msg = message as Messages.InputSuccess;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(msg.reason);
            }
            else
            {
                Console.WriteLine(message);
            }

            Console.ResetColor();
        }
    }
}
