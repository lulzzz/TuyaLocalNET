namespace TuyaLocal.Commands
{
    using Akka.Actor;

    internal class RequestDeviceList
    {
        public RequestDeviceList(IActorRef coordinatorActor) =>
            CoordinatorActor = coordinatorActor;

        public IActorRef CoordinatorActor { get; }
    }
}
