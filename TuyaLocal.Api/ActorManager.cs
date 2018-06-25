namespace TuyaLocal.Api
{
    using Actors;
    using Akka.Actor;

    public class ActorManager
    {
        public ActorManager(IActorRefFactory actorSystem)
        {
            DeviceCoordinator =
                actorSystem.ActorOf(
                    Props.Create<DeviceCoordinator>(),
                    nameof(DeviceCoordinator));

            GroupCoordinator =
                actorSystem.ActorOf(
                    Props.Create<GroupCoordinator>(),
                    nameof(GroupCoordinator));
        }

        public IActorRef DeviceCoordinator { get; }
        public IActorRef GroupCoordinator { get; }
    }
}
