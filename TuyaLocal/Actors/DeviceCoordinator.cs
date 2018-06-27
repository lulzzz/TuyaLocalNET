namespace TuyaLocal.Actors
{
    using System.Collections.Generic;
    using System.Linq;
    using Akka.Actor;
    using Akka.Event;
    using Commands.Device;
    using Models;

    public class DeviceCoordinator : ReceiveActor
    {
        private readonly List<string> _deviceList =
            new List<string>();

        public DeviceCoordinator()
        {
            var logger = Context.GetLogger();

            Receive<IDeviceCommand>(
                command =>
                {
                    switch (command)
                    {
                        case Add _ when _deviceList.Any(r => r == command.Id):

                            logger.Info(
                                $"Coordinator: {command.Id} can not be added, it already exists.");

                            return;

                        case Add _:
                            _deviceList.Add(command.Id);

                            Context.ActorOf(
                                Props.Create(() => new DeviceActor(command.Id)),
                                command.Id);

                            logger.Info(
                                $"Coordinator has added device {command.Id}");

                            break;

                        case Remove _
                            when _deviceList.All(r => r != command.Id):

                            logger.Info(
                                $"Coordinator: {command.Id} can not be removed, it does not exist.");

                            return;

                        case Remove _:
                            _deviceList.Remove(command.Id);

                            logger.Info(
                                $"Coordinator removed device {command.Id}");

                            break;
                    }

                    var child = Context.Child(command.Id);

                    if (child.Path.Elements.Count != 0)
                    {
                        child.Tell(command, Sender);
                    }
                    else
                    {
                        if (Sender.Path.Name == "deadLetters")
                        {
                            return;
                        }

                        Sender.Tell(null);
                    }
                });

            Receive<GetAll>(
                command =>
                {
                    logger.Info("Coordinator gets all device ids");
                    Sender.Tell(_deviceList);
                });

            Receive<RemoveAll>(
                command =>
                {
                    logger.Info("Coordinator removes all devices");

                    foreach (var device in _deviceList)
                    {
                        Context.Child(device).Tell(new Remove(device));
                    }

                    _deviceList.Clear();
                });
        }
    }
}
