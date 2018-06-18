namespace TuyaLocal.Api.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using Akka.Actor;
    using Commands;
    using Microsoft.AspNetCore.Mvc;

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
            var lol = new AddDevice(body.Id, body.Name, body.Address, body.SecretKey);
            List<ValidationResult> result = ValidateBody(lol);

            if (result.Count > 0)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new JsonResult(result);
            }

            _actorManager.DeviceCoordinator.Tell(lol);

            return new JsonResult(body);
        }

        private List<ValidationResult> ValidateBody(object body)
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

    public class AddPayload
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string SecretKey { get; set; }
    }
}
