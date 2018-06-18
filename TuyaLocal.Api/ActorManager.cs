namespace TuyaLocal.Api
{
    using Actors;
    using Akka.Actor;

    public class ActorManager
    {
        public ActorManager(IActorRefFactory actorSystem) => DeviceCoordinator =
            actorSystem.ActorOf(
                Props.Create<DeviceCoordinator>(),
                nameof(DeviceCoordinator));

        public IActorRef DeviceCoordinator { get; }
    }
}
