namespace TuyaLocal.Commands
{
    using Akka.Actor;

    public class RequestDeviceList
    {
        public RequestDeviceList(IActorRef coordinatorActor) =>
            CoordinatorActor = coordinatorActor;

        public IActorRef CoordinatorActor { get; }
    }
}
