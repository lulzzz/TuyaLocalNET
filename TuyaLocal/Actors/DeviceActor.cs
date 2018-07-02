namespace TuyaLocal.Actors
{
    using Akka.Actor;
    using Akka.Event;
    using Commands.Device;
    using Core.Models;
    using Models;

    public class DeviceActor : ReceiveActor
    {
        public DeviceActor(string id)
        {
            var device = new TuyaDevice {Id = id};

            var logger = Context.GetLogger();

            Receive<Add>(
                command =>
                {
                    device.Name = command.Name;
                    device.IpAddress = command.IpAddress;
                    device.SecretKey = command.SecretKey;

                    logger.Info(
                        $"{command.Id} actor been added.");
                });

            Receive<Update>(
                command =>
                {
                    device.Name = command.Name;
                    device.IpAddress = command.IpAddress;
                    device.SecretKey = command.SecretKey;

                    logger.Info(
                        $"{command.Id} actor has been updated.");
                });

            Receive<Get>(
                command =>
                {
                    logger.Info(
                        $"{command.Id} actor getting Device");

                    device.GetInfo();

                    Sender.Tell(device);
                });

            Receive<Remove>(
                command =>
                {
                    logger.Info(
                        $"{command.Id} actor is taking a poison pill.");

                    Self.Tell(PoisonPill.Instance);
                });
        }
    }
}
