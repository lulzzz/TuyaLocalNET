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
                    _deviceList.Add(
                        new Device
                        {
                            Name = command.Name,
                            Id = command.Id,
                            Address = command.IpAddress,
                            Key = command.SecretKey
                        });

                    logger.Info(
                        $"{command.Name} has been added: {command.Id}");
                });

            Receive<RemoveDevice>(
                command =>
                {
                    if (_deviceList.Count(
                            r => string.Equals(r.Id, command.Id)) ==
                        0)
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

            Receive<ListDevices>(
                command => { Sender.Tell(new ListData(_deviceList)); });
        }
    }
}
