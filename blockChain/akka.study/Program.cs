using Akka.Actor;
using System;

namespace akka.study
{
    partial class Program
    {
        static void Main(string[] args)
        {
           var MyActorSystem = ActorSystem.Create("MyActorSystem");

            var helloactor = MyActorSystem.ActorOf(Props.Create(()=>new Helloactor()));
            helloactor.Tell("");

            var consoleWriterActor = MyActorSystem.ActorOf(Props.Create(() =>
                new ConsoleWriterActor()));
            var consoleReaderActor = MyActorSystem.ActorOf(Props.Create(() =>
                new ConsoleReaderActor(consoleWriterActor)));

            consoleReaderActor.Tell("start");

            MyActorSystem.WhenTerminated.Wait();
        }
    }

    internal class ConsoleReaderActor : UntypedActor
    {
        private IActorRef consoleWriterActor;

        public ConsoleReaderActor(IActorRef consoleWriterActor)
        {
            this.consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object message)
        {
            var read = Console.ReadLine();
            consoleWriterActor.Tell(read);
            Self.Tell("continue");
        }
    }

    internal class ConsoleWriterActor:UntypedActor
    {
        public ConsoleWriterActor()
        {

        }

        protected override void OnReceive(object message)
        {
            var msg = message as string;
            if (string.IsNullOrEmpty(msg))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Please provide an input.\n");
                Console.ResetColor();
                return;
            }else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("typed:"+msg);
                Console.ResetColor();
            }
        }
    }
}
