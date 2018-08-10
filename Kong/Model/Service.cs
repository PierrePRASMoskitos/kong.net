using System.Collections.Generic;
using System.Threading.Tasks;
using Kong.Interop;
using Kong.Slumber;

namespace Kong.Model
{
    public class Service : IService
    {
        public string Id { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int ConnectTimeout { get; set; }
        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int Retries { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }

        public IPlugins Plugins { get; }

        private IRequestFactory requestFactory_;

        public async Task<IService> Create()
        {
            return await requestFactory_.Post<Service>(this).ConfigureAwait(false);
        }

        public async Task<IService> Update()
        {
            return await requestFactory_.Patch<Service>(this).ConfigureAwait(false);
        }

        public async Task<IService> Get()
        {
            return await requestFactory_.Get<Service>().ConfigureAwait(false);
        }

        public Task Delete()
        {
            return requestFactory_.Delete();
        }

        public void Configure(IRequestFactory requestFactory)
        {
            requestFactory_ = requestFactory.Create("/{id}", new Dictionary<string, string> { { "id", Id ?? string.Empty } });
        }
    }
}