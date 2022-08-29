using System;

namespace Template.Domain.DTOs
{
    public record UserDTO
    {
        //public UserDTO(Guid id) => (Id) = (id);

        public void Deconstruct(out Guid id, out string name) => (id, name) = (Id, Name);

        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
