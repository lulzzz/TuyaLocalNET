namespace TuyaLocal.Api.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
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

        [HttpDelete("{groupName}")]
        public IActionResult Delete(string groupName)
        {
            return ValidateCommand(
                new Commands.Group.Delete(groupName),
                _actorManager.GroupCoordinator);
        }

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            return ValidateCommand(
                new Commands.Group.DeleteAll(),
                _actorManager.GroupCoordinator);
        }
    }
}
