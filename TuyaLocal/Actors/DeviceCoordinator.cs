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
        private readonly List<Device> _deviceList = new List<Device>();

        public DeviceCoordinator()
        {
            var logger = Context.GetLogger();

            Receive<Add>(
                command =>
                {
                    if (_deviceList.Any(r => r.Id == command.Id))
                    {
                        logger.Info(
                            $"{command.Id} already exists.");

                        return;
                    }

                    _deviceList.Add(
                        new Device
                        {
                            Name = command.Name,
                            Id = command.Id,
                            IpAddress = command.IpAddress,
                            SecretKey = command.SecretKey
                        });

                    logger.Info(
                        $"{command.Name} has been added: {command.Id}");
                });

            Receive<Update>(
                command =>
                {
                    if (_deviceList.All(r => r.Id != command.Id))
                    {
                        logger.Info(
                            $"Tried to update not existing device {command.Id}");

                        return;
                    }

                    var updatableDevice =
                        _deviceList.Single(r => r.Name != null);

                    if (updatableDevice == null)
                    {
                        return;
                    }

                    updatableDevice.Name = command.Name;
                    updatableDevice.IpAddress = command.IpAddress;
                    updatableDevice.SecretKey = command.SecretKey;

                    logger.Info(
                        $"{command.Id} has been updated.");
                });

            Receive<Remove>(
                command =>
                {
                    if (_deviceList.All(r => r.Id != command.Id))
                    {
                        logger.Info(
                            $"Tried to remove not existing device: {command.Id}");

                        return;
                    }

                    _deviceList.Remove(
                        _deviceList.Single(
                            r => r.Id == command.Id));

                    logger.Info(
                        $"A device has been removed: {command.Id}");
                });

            Receive<Get>(
                command =>
                {
                    if (_deviceList.All(r => r.Id != command.Id))
                    {
                        logger.Info(
                            $"Tried to get not existing device: {command.Id}");

                        Sender.Tell(null);

                        return;
                    }

                    logger.Info($"Getting device: {command.Id}");

                    Sender.Tell(_deviceList.Single(r => r.Id == command.Id));
                });

            Receive<GetAll>(
                command =>
                {
                    logger.Info("Getting devices");
                    Sender.Tell(_deviceList);
                });

            Receive<RemoveAll>(
                command =>
                {
                    logger.Info("Removing all devices");
                    _deviceList.Clear();
                });
        }
    }
}
