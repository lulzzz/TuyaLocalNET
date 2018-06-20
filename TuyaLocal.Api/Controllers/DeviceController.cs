namespace TuyaLocal.Api.Controllers
{
    using System.Collections.Generic;
    using Akka.Actor;
    using Commands;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using TuyaLocal.Models;

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
            var result = _actorManager.DeviceCoordinator
                .Ask<IEnumerable<Device>>(
                    new GetDevices())
                .Result;

            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(string id)
        {
            var result = _actorManager.DeviceCoordinator
                .Ask<Device>(
                    new GetDevice(id))
                .Result;

            return new JsonResult(result);
        }

        [HttpPost("{id}")]
        public IActionResult Edit(string id, [FromBody] EditPayload body) =>
            ValidateCommand(
                new UpdateDevice(
                    id,
                    body.Name,
                    body.Address,
                    body.SecretKey),
                _actorManager.DeviceCoordinator);

        [HttpDelete("{id}")]
        public IActionResult Remove(string id) =>
            ValidateCommand(
                new RemoveDevice(id),
                _actorManager.DeviceCoordinator);
    }
}
