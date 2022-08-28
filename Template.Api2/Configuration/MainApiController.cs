using Template.Shared.Core.Communication.Mediator;
using Template.Shared.Core.Messages.Notifications;
using Template.Shared.Core.Messages.Notifications.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Template.Api.Configuration
{
    [ApiController]
    public abstract class MainApiController : ControllerBase
    {
        protected IEnumerable<long> AppsId { get; set; }

        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;
        protected MainApiController(INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        /// <summary>
        /// Check Operation
        /// </summary>
        /// <returns></returns>
        protected bool CheckOperation() => !_notifications.ExistsNotification();

        /// <summary>
        /// Get Error Messages
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<string> GetErrorMessages() => _notifications.GetNotifications().Select(c => c.Value).ToList();

        /// <summary>
        /// Used for asynchronous processing (sending a file for processing eg where the file was received and will be processed at
        /// some point)
        /// </summary>
        /// <returns></returns>
        protected IActionResult ResponseAccepted()
        {
            if (!this.CheckOperation()) return ResponseErrorNotifications();

            return Accepted();
        }

        /// <summary>
        /// Used in resource creation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="route"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult ResponsePost<T>(string action, object route, T result)
        {
            if (!this.CheckOperation()) return ResponseErrorNotifications();

            return result is not null ? CreatedAtAction(action, route, result) : NoContent();
        }

        /// <summary>
        /// Used in resource deletion
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult ResponseDelete(object result = null)
        {
            if (!this.CheckOperation()) return ResponseErrorNotifications();

            return result is not null ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Used in resource query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult ResponseGet<T>(T result)
        {
            if (!this.CheckOperation()) return ResponseErrorNotifications();

            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Used in resource update
        /// </summary>
        /// <returns></returns>
        protected IActionResult ResponsePutPatch() => NoContent();

        /// <summary>
        /// Used for unauthorized requests
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult ResponseUnauthorized(string message) => Unauthorized(message);

        /// <summary>
        /// Used in error responses
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult ResponseError(string message) => BadRequest(message);

        /// <summary>
        /// Used in error notifications (notification pattern)
        /// </summary>
        /// <returns></returns>
        private IActionResult ResponseErrorNotifications() => BadRequest(_notifications.GetNotificationsByValueArray());
    }
}
