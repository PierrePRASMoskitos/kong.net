using System.Threading.Tasks;
using Kong.Model;

namespace Kong.Interop
{
    public interface IRoutes
    {
        Task<ResourceCollection<IRoute>> List();
        Task<IRoute> Create(IRoute route);
        Task<IRoute> Update(IRoute route);
        Task<IRoute> Get(IRoute route);
    }
}