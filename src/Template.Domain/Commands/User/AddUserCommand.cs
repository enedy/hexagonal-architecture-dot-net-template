using Template.Shared.Core.Messages;
using FluentValidation;
using Template.Domain.Resources;
using System;

namespace Template.Domain.Commands
{
    public class AddUserCommand : Command<Guid>
    {
        public string Name { get; private set; }

        public AddUserCommand(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AddUserCommandValidation : AbstractValidator<AddUserCommand>
        {
            public AddUserCommandValidation()
            {
                RuleFor(c => c.Name).NotEmpty().WithMessage(Messages.RequiredName);
            }
        }
    }
}
