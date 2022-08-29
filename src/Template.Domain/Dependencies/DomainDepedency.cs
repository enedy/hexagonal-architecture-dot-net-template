using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Template.Domain.Commands;
using Template.Domain.DTOs;
using Template.Domain.Events;

namespace Template.Domain.Dependencies
{
    public static class DomainDepedency
    {
        public static void AddDomainModule(this IServiceCollection services)
        {
            // Commands
            services.AddScoped<IRequestHandler<AddUserCommand, Guid>, AddUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePartialUserCommand, bool>, UpdatePartialUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, bool>, UpdateUserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<UserCreatedEvent>, UserCreatedEventHandler>();

            // Queries
            services.AddScoped<IRequestHandler<GetUsersQueries, IEnumerable<UserDTO>>, GetUsersQueriesHandler>();
            services.AddScoped<IRequestHandler<GetUserByIdQueries, UserDTO>, GetUserByIdQueriesHandler>();
        }
    }
}
