namespace TuyaLocal.Actors
{
    using System.Collections.Generic;
    using System.Linq;
    using Akka.Actor;
    using Akka.Event;
    using Commands;
    using Models;

    internal class DeviceCoordinator : ReceiveActor
    {
        private readonly List<Device> _deviceList = new List<Device>();

        public DeviceCoordinator()
        {
            var logger = Context.GetLogger();

            Receive<AddDevice>(
                command =>
                {
                    _deviceList.Add(
                        command.Device);

                    logger.Info(
                        $"{command.Device.DeviceName} has been added: {command.Device.DeviceId}");
                });

            Receive<RemoveDevice>(
                command =>
                {
                    _deviceList.Remove(
                        _deviceList.Single(
                            r => r.DeviceId == command.DeviceId));

                    logger.Info(
                        $"A device has been removed: {command.DeviceId}");
                });


            Receive<ListDevices>(
                command =>
                {
                    Sender.Tell(new ListData(_deviceList));
                });
        }
    }
}
