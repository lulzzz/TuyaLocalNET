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

    /// <inheritdoc />
    [Route("api/groups")]
    public class GroupController : ApiControllerBase
    {
        private readonly ActorManager _actorManager;

        /// <summary>
        ///     Controller for managing the group actors
        /// </summary>
        public GroupController(ActorManager actorManager) =>
            _actorManager = actorManager;

        /// <summary>
        ///     Adds a new group
        /// </summary>
        /// <param name="createPayload">Payload (Create)</param>
        /// <returns>Returns a newly created group</returns>
        /// <response code="200">Returns the newly created group</response>
        /// <response code="400">If the payload is malformed</response>
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Create([FromBody] Create createPayload) =>
            ValidateCommand(
                new Commands.Group.Create(createPayload.Name),
                _actorManager.GroupCoordinator);

        /// <summary>
        ///     Adds a device (id) to a group
        /// </summary>
        /// <param name="groupName">GroupName (string)</param>
        /// <param name="addDevicePayload">Payload (AddDevice)</param>
        /// <returns>Returns the group containing the added device</returns>
        /// <response code="200">Returns the group containing the added device</response>
        /// <response code="400">If the payload is malformed</response>
        [HttpPost("{groupName}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult AddDevice(
            string groupName,
            [FromBody] AddDevice addDevicePayload) => ValidateCommand(
            new Commands.Group.AddDevice(groupName, addDevicePayload.Id),
            _actorManager.GroupCoordinator);

        /// <summary>
        ///     Removes a device from a group
        /// </summary>
        /// <param name="groupName">GroupName (string)</param>
        /// <param name="deviceName">DeviceName (string)</param>
        /// <returns>Returns the group and the removed device</returns>
        /// <response code="200">Returns the group and the removed device</response>
        /// <response code="400">If the payload is malformed</response>
        [HttpDelete("{groupName}/{deviceName}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult
            RemoveDevice(string groupName, string deviceName) =>
            ValidateCommand(
                new RemoveDevice(groupName, deviceName),
                _actorManager.GroupCoordinator);

        /// <summary>
        ///     Gets all groups
        /// </summary>
        /// <returns>Returns a list of all groups</returns>
        /// <response code="200">Returns the group list</response>
        /// <response code="404">If there are no groups in the group list</response>
        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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

        /// <summary>
        ///     Gets a group
        /// </summary>
        /// <param name="groupName">GroupName (string)</param>
        /// <returns>Returns a single group</returns>
        /// <response code="200">Returns the requested group</response>
        /// <response code="404">If there is no group with the requested name in the group list</response>
        [HttpGet("{groupName}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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

        /// <summary>
        ///     Deletes a group
        /// </summary>
        /// <param name="groupName">GroupName (string)</param>
        /// <returns>Returns the removed group name</returns>
        /// <response code="200">Returns the removed group name</response>
        /// <response code="400">If the GroupName parameter is malformed</response>
        [HttpDelete("{groupName}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Delete(string groupName) => ValidateCommand(
            new Delete(groupName),
            _actorManager.GroupCoordinator);

        /// <summary>
        ///     Deletes all groups
        /// </summary>
        /// <returns>Returns an empty array</returns>
        /// <response code="200">Returns an empty array</response>
        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public IActionResult DeleteAll() => ValidateCommand(
            new DeleteAll(),
            _actorManager.GroupCoordinator);
    }
}
