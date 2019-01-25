using Akka.Actor;
using System;

namespace akka.study
{
    internal class ConsoleReaderActor : UntypedActor
    {
        public const string StartCommand = "start";
        public const string ExitCommand = "exit";

        private IActorRef _validationActor;

        public ConsoleReaderActor()
        {
            //this._validationActor = validationActor;
        }

        protected override void OnReceive(object message)
        {
            if(message.Equals(StartCommand))
            {
                DoPrintInstructions();
            }

            GetAndValidateInput();
        }

        private void DoPrintInstructions()
        {
            Console.WriteLine("Please provide the URI of a log file on disk.\n");
        }
        private void GetAndValidateInput()
        {
            var message = Console.ReadLine();
            if (!string.IsNullOrEmpty(message) &&
            String.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                // if user typed ExitCommand, shut down the entire actor
                // system (allows the process to exit)
                Context.System.Terminate();
                return;
            }
            else
            {
                //_validationActor.Tell(message);
                Context.ActorSelection("akka://MyActorSystem/user/validationActor").Tell(message);
            }
        }
    }
}
