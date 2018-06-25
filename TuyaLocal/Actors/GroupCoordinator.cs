namespace TuyaLocal.Actors
{
    using System.Collections.Generic;
    using System.Linq;
    using Akka.Actor;
    using Akka.Event;
    using Commands.Group;
    using Models;

    public class GroupCoordinator : ReceiveActor
    {
        private readonly List<Group> _groupList =
            new List<Group>();

        public GroupCoordinator()
        {
            var logger = Context.GetLogger();

            Receive<Create>(
                command =>
                {
                    if (_groupList.Any(r => r.Name == command.Name))
                    {
                        logger.Info($"Group {command.Name} already exists.");

                        return;
                    }

                    _groupList.Add(
                        new Group
                            {Name = command.Name});

                    logger.Info($"Group {command.Name} has been added.");
                });

            Receive<AddDevice>(
                command =>
                {
                    logger.Info(
                        $"Device {command.Id} has been added to {command.GroupName}");
                });
        }
    }
}
