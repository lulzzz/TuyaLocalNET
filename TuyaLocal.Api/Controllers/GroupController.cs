namespace TuyaLocal.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Akka.Actor;
    using Commands.Group;
    using Microsoft.AspNetCore.Mvc;
    using TuyaLocal.Models;
    using AddDevice = Models.Payloads.Group.AddDevice;
    using Create = Models.Payloads.Group.Create;

    [Route("api/groups")]
    public class GroupController : ApiControllerBase
    {
        private readonly ActorManager _actorManager;

        public GroupController(ActorManager actorManager)
        {
            _actorManager = actorManager;
        }

        [HttpPut]
        public IActionResult Create([FromBody] Create body)
        {
            return ValidateCommand(
                new Commands.Group.Create(body.Name),
                _actorManager.GroupCoordinator);
        }

        [HttpPost("{groupName}")]
        public IActionResult AddDevice(
            string groupName,
            [FromBody] AddDevice body)
        {
            return ValidateCommand(
                new Commands.Group.AddDevice(groupName, body.Id),
                _actorManager.GroupCoordinator);
        }

        [HttpDelete("{groupName}/{deviceName}")]
        public IActionResult RemoveDevice(string groupName, string deviceName)
        {
            return ValidateCommand(
                new RemoveDevice(groupName, deviceName),
                _actorManager.GroupCoordinator);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _actorManager.GroupCoordinator
                .Ask<IEnumerable<Group>>(
                    new GetAll())
                .Result;

            if (!result.Any())
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }

            return new JsonResult(result);
        }

        [HttpGet("{groupName}")]
        public IActionResult Get(string groupName)
        {
            var result = _actorManager.GroupCoordinator
                .Ask<Group>(
                    new Get(groupName))
                .Result;

            if (result != null)
            {
                return new JsonResult(result);
            }

            HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;

            return new JsonResult(new List<Group>());
        }

        [HttpDelete("{groupName}")]
        public IActionResult Delete(string groupName)
        {
            return ValidateCommand(
                new Delete(groupName),
                _actorManager.GroupCoordinator);
        }

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            return ValidateCommand(
                new DeleteAll(),
                _actorManager.GroupCoordinator);
        }
    }
}
