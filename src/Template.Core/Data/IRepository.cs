using System;
using Template.Shared.Core.DomainObjects;

namespace Template.Shared.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
