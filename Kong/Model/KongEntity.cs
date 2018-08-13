using System.Collections.Generic;
using System.Threading.Tasks;
using Kong.Interop;
using Kong.Slumber;

namespace Kong.Model
{
    public abstract class KongEntity : IKongEntity
    {
        protected IRequestFactory RequestFactory;
        protected IPlugins Plugins { get; }

        protected KongEntity(IRequestFactory requestFactory)
        {
            RequestFactory = requestFactory;
        }

        public async Task<ResourceCollection<T>> List<T>()
        {
            return await RequestFactory.List<ResourceCollection<T>>(null).ConfigureAwait(false);
        }

        public async Task<T> CreateAsync<T>(T data)
        {
            return await RequestFactory.Post<T>(data).ConfigureAwait(false);
        }

        public async Task<T> UpdateAsync<T>(T data)
        {
            return await RequestFactory.Patch<T>(data).ConfigureAwait(false);
        }

        public async Task<T> GetEntityAsync<T>(string id)
        {
            Configure(RequestFactory, id);
            return await RequestFactory.Get<T>().ConfigureAwait(false);
        }

        public async Task Delete<T>(string id)
        {
            Configure(RequestFactory, id);
            await RequestFactory.Delete().ConfigureAwait(false);
        }

        public void Configure(IRequestFactory requestFactory, string id = null)
        {
            RequestFactory = requestFactory.Create("/{id}", new Dictionary<string, string> { { "id", id ?? string.Empty } });
        }
    }
}