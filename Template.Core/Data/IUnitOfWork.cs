using System.Threading.Tasks;

namespace Template.Shared.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
