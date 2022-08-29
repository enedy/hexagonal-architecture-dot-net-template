using Template.Shared.Core.Messages;
using FluentValidation;
using Template.Domain.Resources;
using System;

namespace Template.Domain.Commands
{
    public class UpdateUserCommand : Command<bool>
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }

        public UpdateUserCommand(string name)
        {
            Name = name;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
        {
            public UpdateUserCommandValidation()
            {
                RuleFor(c => c.Id).NotEmpty().WithMessage(Messages.RequiredId);
            }
        }
    }
}
