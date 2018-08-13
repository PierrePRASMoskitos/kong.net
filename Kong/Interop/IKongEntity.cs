using System.Threading.Tasks;
using Kong.Model;
using Kong.Slumber;

namespace Kong.Interop
{
    public interface IKongEntity
    {
        Task<ResourceCollection<T>> List<T>();
        Task<T> CreateAsync<T>(T data);
        Task<T> UpdateAsync<T>(T data);
        Task<T> GetEntityAsync<T>(string id);
        Task Delete<T>(string id);

        void Configure(IRequestFactory requestFactory, string id = null);
    }
}