using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Template.Api.Configuration;
using Template.Domain.Commands;
using Template.Domain.DTOs;
using Template.Shared.Core.Communication.Mediator;
using Template.Shared.Core.Messages.Notifications;

namespace Template.Api.v1.Controllers
{
    [ApiVersion("1", Deprecated = true)]
    [Route("api/v{version:apiVersion}/template")]
    public class TemplateController : MainApiController
    {
        private readonly IMediatorHandler _mediatorHandler;

        private readonly ILogger<MainApiController> _logger;

        public TemplateController(INotificationHandler<DomainNotification> notifications,
        IMediatorHandler mediatorHandler, ILogger<MainApiController> logger) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _logger = logger;
        }

        /// <summary>
        /// Example GET
        /// </summary>
        /// <returns></returns>
        [Route(""), HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = null, Type = typeof(UserDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = null, Type = typeof(string))]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Input Log");
            var users = await _mediatorHandler.SendQuery(new GetUsersQueries(""));

            return ResponseGet(users);
        }

        /// <summary>
        /// Example POST
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("{name}"), HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, Description = null, Type = typeof(UserDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = null, Type = typeof(string))]
        public async Task<IActionResult> PostSynchronization(string name)
        {
            _logger.LogInformation("Input Log");

            var command = new AddUserCommand("");
            var id = await _mediatorHandler.SendCommand(command);

            // PASSA O METODO GET
            // PASSA O PARAMETRO DO METODO GET (id)
            // PASSA O OBJETO QUE FOI CADASTRADO E DEPOIS CONSULTADO DO BANCO COMO RETORNO
            return ResponsePost(nameof(GetAsync), new { id = id }, new UserDTO());
        }
    }
}
