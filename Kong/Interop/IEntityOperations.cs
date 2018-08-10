using System.Threading.Tasks;

namespace Kong.Interop
{
    public interface IEntityOperations
    {
        Task<T> Create<T>();
        Task<T> Update<T>();
        Task<T> Get<T>();
        Task Delete();
    }
}