using System;

namespace Template.Domain.Entities.Interfaces
{
    public interface IUpdate
    {
        public Guid? UpdateUserId { get; }
        public DateTime? UpdateDate { get; }
        public string UpdateProgram { get; }
    }
}
