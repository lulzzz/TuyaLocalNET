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
        public IActionResult Add([FromBody] AddPayload body)
        {
            var command = new AddDevice(body.Id, body.Name, body.Address, body.SecretKey);
            List<ValidationResult> result = ValidateBody(command);

            if (result.Count > 0)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new JsonResult(result);
            }

            _actorManager.DeviceCoordinator.Tell(command);

            return new JsonResult(body);
        }

        private static List<ValidationResult> ValidateBody(object body)
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
