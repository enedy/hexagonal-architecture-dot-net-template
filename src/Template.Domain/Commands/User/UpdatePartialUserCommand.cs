using Template.Shared.Core.Messages;
using FluentValidation;
using Template.Domain.Resources;
using System;
using Microsoft.AspNetCore.JsonPatch;
using Template.Domain.Entities;

namespace Template.Domain.Commands
{
    public class UpdatePartialUserCommand : Command<bool>
    {
        public Guid Id { get; private set; }
        public JsonPatchDocument<User> User { get; private set; }

        public UpdatePartialUserCommand(Guid id, JsonPatchDocument<User> user)
        {
            Id = id;
            User = user;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePartialUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class UpdatePartialUserCommandValidation : AbstractValidator<UpdatePartialUserCommand>
        {
            public UpdatePartialUserCommandValidation()
            {
                RuleFor(c => c.Id).NotEmpty().WithMessage(Messages.RequiredId);
            }
        }
    }
}
