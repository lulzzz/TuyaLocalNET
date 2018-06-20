namespace TuyaLocal.Actors
{
    using System.Collections.Generic;
    using System.Linq;
    using Akka.Actor;
    using Akka.Event;
    using Commands;
    using Models;

    public class DeviceCoordinator : ReceiveActor
    {
        private readonly List<Device> _deviceList = new List<Device>();

        public DeviceCoordinator()
        {
            var logger = Context.GetLogger();

            Receive<AddDevice>(
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

            Receive<UpdateDevice>(
                command =>
                {
                    if (_deviceList.Any(r => r.Id != command.Id))
                    {
                        logger.Info(
                            $"{command.Id} does not exist.");

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

            Receive<RemoveDevice>(
                command =>
                {
                    if (_deviceList.Any(r => r.Id != command.Id))
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

            Receive<GetDevice>(
                command =>
                {
                    if (_deviceList.Any(r => r.Id != command.Id))
                    {
                        logger.Info(
                            $"Tried to get not existing device: {command.Id}");

                        Sender.Tell(new Device());

                        return;
                    }

                    Sender.Tell(_deviceList.Single(r => r.Id == command.Id));
                });

            Receive<GetDevices>(
                command => { Sender.Tell(_deviceList); });
        }
    }
}
