using Akka.Actor;
using System;

namespace akka.study
{
    internal class ConsoleReaderActor : UntypedActor
    {
        public const string StartCommand = "start";
        public const string ExitCommand = "exit";

        private IActorRef _validationActor;

        public ConsoleReaderActor(IActorRef validationActor)
        {
            this._validationActor = validationActor;
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
            Console.WriteLine("Write whatever you want into the console!");
            Console.WriteLine("Some entries will pass validation, and some won't...\n\n");
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
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
                _validationActor.Tell(message);
            }
        }
    }
}
