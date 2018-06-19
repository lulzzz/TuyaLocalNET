namespace TuyaLocal.Api.Controllers
{
    using Akka.Actor;
    using Commands;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/devices")]
    public class DeviceController : ApiControllerBase
    {
        private readonly ActorManager _actorManager;

        public DeviceController(ActorManager actorManager) =>
            _actorManager = actorManager;

        [HttpPut]
        public IActionResult Add([FromBody] AddPayload body) =>
            ValidateCommand(
                new AddDevice(
                    body.Id,
                    body.Name,
                    body.Address,
                    body.SecretKey),
                _actorManager.DeviceCoordinator);

        [HttpGet]
        public IActionResult Get()
        {
            var result = _actorManager.DeviceCoordinator.Ask<ListData>(
                    new RequestDeviceList())
                .Result;

            return new JsonResult(result.Devices);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id) =>
            ValidateCommand(
                new RemoveDevice(id),
                _actorManager.DeviceCoordinator);
    }
}
