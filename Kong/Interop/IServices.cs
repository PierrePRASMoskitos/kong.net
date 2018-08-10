using System.Threading.Tasks;
using Kong.Model;

namespace Kong.Interop
{
    public interface IServices
    {
        Task<ResourceCollection<IService>> List();
        Task<IService> Create(IService service);
        Task<IService> Update(IService service);
        Task<IService> Get(IService service);
    }
}