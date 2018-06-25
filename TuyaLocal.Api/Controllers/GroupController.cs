namespace TuyaLocal.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Payloads.Group;

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
        public IActionResult AddDevice(string groupName, [FromBody] AddDevice body)
        {
            return ValidateCommand(
                new Commands.Group.AddDevice(groupName, body.Id),
                _actorManager.GroupCoordinator);
        }
    }
}
