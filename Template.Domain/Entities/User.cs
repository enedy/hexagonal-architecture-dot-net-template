using Template.Shared.Core.DomainObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Domain.Entities.Interfaces;

namespace Template.Domain.Entities
{
    [Table("TAV_TRABAL")]
    public class User : Entity, IAggregateRoot, ICreation, IUpdate
    {
        protected User()
        {
        }

        public User(string name)
        {
            Name = name;
            CreationUserId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            CreationProgram = "Template";
        }

        public string Name { get; private set; }
        public Guid CreationUserId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string CreationProgram { get; private set; }
        public Guid? UpdateUserId { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public string UpdateProgram { get; private set; }

        public void UpdateName(string name, string updateProgram)
        {
            Name = name;

            Audit(updateProgram);
        }

        public void Audit(string updateProgram)
        {
            UpdateUserId = Guid.NewGuid();
            UpdateDate = DateTime.UtcNow;
            UpdateProgram = updateProgram;
        }
    }
}
