namespace TuyaLocal.Api.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using Akka.Actor;
    using Microsoft.AspNetCore.Mvc;

    public abstract class ApiControllerBase : ControllerBase
    {
        public IActionResult ValidateCommand(
            object command,
            IActorRef actor)
        {
            var result = ValidatePayloadBody(command);

            if (result.Count > 0)
            {
                HttpContext.Response.StatusCode =
                    (int) HttpStatusCode.BadRequest;

                return new JsonResult(result);
            }

            actor.Tell(command);

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
