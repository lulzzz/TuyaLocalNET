namespace TuyaLocal.Actors
{
    using System.Collections.Generic;
    using Akka.Actor;
    using Commands;
    using Models;

    public class DeviceListActor : ReceiveActor
    {
        public DeviceListActor(List<Device> result)
        {
            Receive<ListData>(
                command =>
                {
                    result.AddRange(command.Devices);
                });

            Receive<RequestDeviceList>(
                command =>
                {
                    command.CoordinatorActor.Tell(new ListDevices());
                });
        }
    }
}
