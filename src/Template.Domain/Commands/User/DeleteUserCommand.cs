using Template.Shared.Core.Messages;
using FluentValidation;
using Template.Domain.Resources;
using System;

namespace Template.Domain.Commands
{
    public class DeleteUserCommand : Command<bool>
    {
        public Guid Id { get; private set; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class DeleteUserCommandValidation : AbstractValidator<DeleteUserCommand>
        {
            public DeleteUserCommandValidation()
            {
                RuleFor(c => c.Id).NotEmpty().WithMessage(Messages.RequiredId);
            }
        }
    }
}
