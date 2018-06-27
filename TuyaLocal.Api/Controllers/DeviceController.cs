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

    /// <inheritdoc />
    [Route("api/devices")]
    public class DeviceController : ApiControllerBase
    {
        private readonly ActorManager _actorManager;

        /// <summary>
        ///     Controller for managing the device actors
        /// </summary>
        public DeviceController(ActorManager actorManager) =>
            _actorManager = actorManager;

        /// <summary>
        ///     Adds a new device
        /// </summary>
        /// <param name="addPayload">Payload (Add)</param>
        /// <returns>Returns a newly created device</returns>
        /// <response code="200">Returns the newly created device</response>
        /// <response code="400">If the payload is malformed</response>
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Add([FromBody] Add addPayload) =>
            ValidateCommand(
                new Commands.Device.Add(
                    addPayload.Id,
                    addPayload.Name,
                    addPayload.Address,
                    addPayload.SecretKey),
                _actorManager.DeviceCoordinator);

        /// <summary>
        ///     Updates Name, Address, SecretKey of a device
        /// </summary>
        /// <param name="deviceId">DeviceId (string)</param>
        /// <param name="editPayload">Payload (Edit)</param>
        /// <returns>Returns the updated device</returns>
        /// <response code="200">Returns the updated device</response>
        /// <response code="400">If the DeviceId parameter or the payload is malformed</response>
        [HttpPost("{deviceId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Edit(
            string deviceId,
            [FromBody] Edit editPayload) =>
            ValidateCommand(
                new Update(
                    deviceId,
                    editPayload.Name,
                    editPayload.Address,
                    editPayload.SecretKey),
                _actorManager.DeviceCoordinator);

        /// <summary>
        ///     Removes a device
        /// </summary>
        /// <param name="deviceId">DeviceId (string)</param>
        /// <returns>Return the removed device id</returns>
        /// <response code="200">Returns the removed device id</response>
        /// <response code="400">If the DeviceId parameter is malformed</response>
        [HttpDelete("{deviceId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult RemoveSingle(string deviceId) =>
            ValidateCommand(
                new Remove(deviceId),
                _actorManager.DeviceCoordinator);

        /// <summary>
        ///     Gets all devices
        /// </summary>
        /// <returns>Returns a list of all devices</returns>
        /// <response code="200">Returns the device list</response>
        /// <response code="404">If there are no devices in the device list</response>
        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            var result = _actorManager.DeviceCoordinator
                .Ask<IEnumerable<string>>(
                    new GetAll())
                .Result;

            if (!result.Any())
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }

            return new JsonResult(result);
        }

        /// <summary>
        ///     Gets a device
        /// </summary>
        /// <param name="deviceId">DeviceId (string)</param>
        /// <returns>Returns a single device</returns>
        /// <response code="200">Returns the requested device</response>
        /// <response code="404">If there is no device with the requested device id in the device list</response>
        [HttpGet("{deviceId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public IActionResult GetSingle(string deviceId)
        {
            var result = _actorManager.DeviceCoordinator
                .Ask<Device>(
                    new Get(deviceId))
                .Result;

            if (result == null)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }

            return new JsonResult(result);
        }

        /// <summary>
        ///     Removes all devices
        /// </summary>
        /// <returns>Returns an empty array</returns>
        /// <response code="200">Returns an empty array</response>
        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public IActionResult Remove() =>
            ValidateCommand(
                new RemoveAll(),
                _actorManager.DeviceCoordinator);
    }
}
