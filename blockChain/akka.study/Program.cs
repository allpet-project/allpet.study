using Akka.Actor;

namespace akka.study
{
    partial class Program
    {
        static void Main(string[] args)
        {
           var MyActorSystem = ActorSystem.Create("MyActorSystem");

            var helloactor = MyActorSystem.ActorOf(Props.Create(()=>new Helloactor()));
            helloactor.Tell("");


            Props consoleWriteProps = Props.Create<ConsoleWriterActor>();
            IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriteProps, "consoleWriterActor");

            //Props validationActorProps = Props.Create<ValidationActor>(consoleWriterActor);
            //IActorRef validationActor = MyActorSystem.ActorOf(validationActorProps,"validationActor");

            Props tailCoordinatorProps = Props.Create(() => new TailCoordinatorActor());
            IActorRef tailCoordinatorActor = MyActorSystem.ActorOf(tailCoordinatorProps,"tailCoordinatorActor");

            // pass tailCoordinatorActor to fileValidatorActorProps (just adding one extra arg)
            Props fileValidatorActorProps = Props.Create(() =>new FileValidatorActor(consoleWriterActor, tailCoordinatorActor));
            IActorRef validationActor = MyActorSystem.ActorOf(fileValidatorActorProps,"validationActor");

            Props consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");

            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            MyActorSystem.WhenTerminated.Wait();
        }
    }


}
