using System;

namespace Template.Domain.Entities.Interfaces
{
    public interface ICreation
    {
        Guid CreationUserId { get; }
        DateTime CreationDate { get; }
        string CreationProgram { get; }
    }
}
