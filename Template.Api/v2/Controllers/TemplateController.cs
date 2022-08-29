using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Template.Api.Configuration;
using Template.Domain.Commands;
using Template.Domain.DTOs;
using Template.Domain.Entities;
using Template.Shared.Core.Communication.Mediator;
using Template.Shared.Core.Messages.Notifications;

namespace Template.Api.v2.Controllers
{
    /// <summary>
    /// Template Controller
    /// </summary>
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/template")]
    public class TemplateController : MainApiConfiguration
    {
        private readonly IMediatorHandler _mediatorHandler;

        private readonly ILogger<MainApiConfiguration> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediatorHandler"></param>
        /// <param name="logger"></param>
        public TemplateController(INotificationHandler<DomainNotification> notifications,
        IMediatorHandler mediatorHandler, ILogger<MainApiConfiguration> logger) : base(notifications)
        {
            _mediatorHandler = mediatorHandler;
            _logger = logger;
        }

        /// <summary>
        /// Used to get all users
        /// </summary>
        /// <returns></returns>
        [Route("users"), HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = null, Type = typeof(UserDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = null, Type = typeof(string))]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersAsync()
        {
            _logger.LogInformation("Input Log");

            return ResponseGet(await _mediatorHandler.SendQuery(new GetUsersQueries()));
        }

        /// <summary>
        /// Used to get a user by id
        /// </summary>
        /// <returns></returns>
        [Route("users/{id}"), HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = null, Type = typeof(UserDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = null, Type = typeof(string))]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            _logger.LogInformation("Input Log");

            return ResponseGet(await _mediatorHandler.SendQuery(new GetUserByIdQueries(id)));
        }

        /// <summary>
        /// Used to start a sync process
        /// </summary>
        /// <returns></returns>
        [Route("users/accepted"), HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = null, Type = typeof(UserDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = null, Type = typeof(string))]
        [AllowAnonymous]
        public IActionResult AcceptedAsync()
        {
            return ResponseAccepted();
        }

        /// <summary>
        /// Used to create a resource
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("users/{name}"), HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, Description = null, Type = typeof(UserDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = null, Type = typeof(string))]
        public async Task<IActionResult> PostUserSync(string name)
        {
            _logger.LogInformation("Input Log");

            var command = new AddUserCommand(name);
            var id = await _mediatorHandler.SendCommand(command);

            var userDTO = await _mediatorHandler.SendQuery(new GetUserByIdQueries(id));

            return ResponsePost(nameof(GetUserByIdAsync), new { id = id }, userDTO);
        }

        /// <summary>
        /// Used to create a resource (example error)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("users/error/{name}"), HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, Description = null, Type = typeof(UserDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = null, Type = typeof(string))]
        public async Task<IActionResult> PostUserErrorSync(string name)
        {
            _logger.LogInformation("Input Log");

            var command = new AddUserCommand(null);
            var id = await _mediatorHandler.SendCommand(command);

            var userDTO = await _mediatorHandler.SendQuery(new GetUserByIdQueries(id));

            return ResponsePost(nameof(GetUserByIdAsync), new { id = id }, userDTO);
        }

        /// <summary>
        /// Used to delete a resource
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("users/{id}"), HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = null)]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = null)]
        public async Task<IActionResult> DeleteSync(Guid id)
        {
            _logger.LogInformation("Delete resource");

            var command = new DeleteUserCommand(id);
            await _mediatorHandler.SendCommand(command);

            return ResponseDelete();
        }

        /// <summary>
        /// Used to update all user resource
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserCommand"></param>
        /// <returns></returns>
        [Route("users/{id}"), HttpPut]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = null)]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = null)]
        public async Task<IActionResult> PutSync(Guid id, UpdateUserCommand updateUserCommand)
        {
            _logger.LogInformation("Input Log");

            updateUserCommand.SetId(id);
            await _mediatorHandler.SendCommand(updateUserCommand);

            return ResponsePutPatch();
        }

        /// <summary>
        /// Update partially a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("users/partial/{id}"), HttpPatch]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = null)]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = null)]
        public async Task<IActionResult> PatchSync(Guid id, [FromBody] JsonPatchDocument<User> user)
        {
            _logger.LogInformation("Input Log");

            await _mediatorHandler.SendCommand(new UpdatePartialUserCommand(id, user));

            return ResponsePutPatch();
        }
    }
}
