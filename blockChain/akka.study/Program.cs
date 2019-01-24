using Akka.Actor;
using System;

namespace akka.study
{
    partial class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("iot-system"))
            {
                // Create top level supervisor
                var supervisor = system.ActorOf(Props.Create<IotSupervisor>(), "iot-supervisor");
                // Exit the system after ENTER is pressed
                Console.ReadLine();
            }
        }
    }
}
