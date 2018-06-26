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
                    if (_groupList.All(r => r.Name != command.GroupName))
                    {
                        logger.Info(
                            $"Tried to add {command.DeviceId} to not existing group: {command.GroupName}");

                        return;
                    }

                    if (_groupList.Single(r => r.Name == command.GroupName)
                        .Devices.Contains(command.DeviceId))
                    {
                        logger.Info(
                            $"{command.DeviceId} already exists in {command.GroupName}");

                        return;
                    }

                    _groupList.Single(r => r.Name == command.GroupName)
                        .Devices.Add(command.DeviceId);

                    logger.Info(
                        $"Device {command.DeviceId} has been added to {command.GroupName}");
                });

            Receive<RemoveDevice>(
                command =>
                {
                    if (_groupList.All(r => r.Name != command.GroupName))
                    {
                        logger.Info(
                            $"Tried to remove {command.DeviceId} from not existing group: {command.GroupName}");

                        return;
                    }

                    if (!_groupList.Single(r => r.Name == command.GroupName)
                        .Devices.Contains(command.DeviceId))
                    {
                        logger.Info(
                            $"{command.DeviceId} does not exist in {command.GroupName}");

                        return;
                    }

                    _groupList.Single(r => r.Name == command.GroupName)
                        .Devices.Remove(command.DeviceId);

                    logger.Info(
                        $"Device {command.DeviceId} has been removed from {command.GroupName}");
                });

            Receive<Get>(
                command =>
                {
                    if (_groupList.All(r => r.Name != command.GroupName))
                    {
                        logger.Info(
                            $"Tried to get not existing group: {command.GroupName}");

                        Sender.Tell(null);

                        return;
                    }

                    Sender.Tell(
                        _groupList.Single(r => r.Name == command.GroupName));

                    logger.Info($"Getting group {command.GroupName}");
                });

            Receive<GetAll>(
                command =>
                {
                    logger.Info("Getting groups");
                    Sender.Tell(_groupList);
                });

            Receive<Delete>(
                command =>
                {
                    if (_groupList.All(r => r.Name != command.GroupName))
                    {
                        logger.Info(
                            $"Tried to delete not existing group: {command.GroupName}");

                        return;
                    }

                    _groupList.Remove(
                        _groupList.Single(
                            r => r.Name == command.GroupName));

                    logger.Info(
                        $"Group {command.GroupName} has been deleted.");
                }
            );

            Receive<DeleteAll>(
                command =>
                {
                    _groupList.Clear();

                    logger.Info("All groups have been deleted.");
                }
            );
        }
    }
}
