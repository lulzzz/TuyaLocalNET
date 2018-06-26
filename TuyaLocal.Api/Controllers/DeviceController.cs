namespace TuyaLocal.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Akka.Actor;
    using Commands.Device;
    using Microsoft.AspNetCore.Mvc;
    using Models.Payloads.Device;
    using TuyaLocal.Models;
    using Add = Models.Payloads.Device.Add;

    [Route("api/devices")]
    public class DeviceController : ApiControllerBase
    {
        private readonly ActorManager _actorManager;

        public DeviceController(ActorManager actorManager) =>
            _actorManager = actorManager;

        [HttpPut]
        public IActionResult Add([FromBody] Add body) =>
            ValidateCommand(
                new Commands.Device.Add(
                    body.Id,
                    body.Name,
                    body.Address,
                    body.SecretKey),
                _actorManager.DeviceCoordinator);

        [HttpPost("{id}")]
        public IActionResult Edit(string id, [FromBody] Edit body) =>
            ValidateCommand(
                new Update(
                    id,
                    body.Name,
                    body.Address,
                    body.SecretKey),
                _actorManager.DeviceCoordinator);

        [HttpDelete("{id}")]
        public IActionResult RemoveSingle(string id) =>
            ValidateCommand(
                new Remove(id),
                _actorManager.DeviceCoordinator);

        [HttpGet]
        public IActionResult Get()
        {
            var result = _actorManager.DeviceCoordinator
                .Ask<IEnumerable<Device>>(
                    new GetAll())
                .Result;

            if (!result.Any())
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }

            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(string id)
        {
            var result = _actorManager.DeviceCoordinator
                .Ask<Device>(
                    new Get(id))
                .Result;

            if (result == null)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }

            return new JsonResult(result);
        }

        [HttpDelete]
        public IActionResult Remove(string id) =>
            ValidateCommand(
                new RemoveAll(),
                _actorManager.DeviceCoordinator);
    }
}
