namespace TuyaLocal.Api.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using Akka.Actor;
    using Commands;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/devices")]
    public class DeviceController : ControllerBase
    {
        private readonly ActorManager _actorManager;

        public DeviceController(ActorManager actorManager)
        {
            _actorManager = actorManager;
        }

        [HttpPut]
        public IActionResult Add([FromBody] AddPayload body) => ValidateCommand(
            new AddDevice(body.Id, body.Name, body.Address, body.SecretKey));

        [HttpDelete("{id}")]
        public IActionResult Remove(string id) =>
            ValidateCommand(new RemoveDevice(id));

        private IActionResult ValidateCommand(object command)
        {
            List<ValidationResult> result = ValidatePayloadBody(command);

            if (result.Count > 0)
            {
                HttpContext.Response.StatusCode =
                    (int) HttpStatusCode.BadRequest;

                return new JsonResult(result);
            }

            _actorManager.DeviceCoordinator.Tell(command);

            return new JsonResult(command);
        }

        private static List<ValidationResult> ValidatePayloadBody(object body)
        {
            var validationResult = new List<ValidationResult>();

            Validator.TryValidateObject(
                body,
                new ValidationContext(body),
                validationResult,
                true);

            return validationResult;
        }
    }
}
