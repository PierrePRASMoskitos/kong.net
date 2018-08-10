using System.Collections.Generic;
using System.Threading.Tasks;
using Kong.Interop;
using Kong.Slumber;

namespace Kong.Model
{
    public class Route : IRoute
    {
        public string Id { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public string[] Protocols { get; set; }
        public string[] Methods { get; set; }
        public string[] Hosts { get; set; }
        public string[] Paths { get; set; }
        public int RegexPriority { get; set; }
        public bool StripPath { get; set; }
        public bool PreserveHost { get; set; }
        public IService Service { get; set; }
        public IPlugins Plugins { get; }
        private IRequestFactory requestFactory_;

        public Route()
        {
            Service = new Service();    
        }

        public async Task<T> Create<T>()
        {
            return await requestFactory_.Post<T>(this).ConfigureAwait(false);
        }

        public async Task<T> Update<T>()
        {
            return await requestFactory_.Patch<T>(this).ConfigureAwait(false);
        }

        public async Task<T> Get<T>()
        {
            return await requestFactory_.Get<T>().ConfigureAwait(false);
        }

        public async Task Delete()
        {
            await requestFactory_.Delete().ConfigureAwait(false);
        }

        public void Configure(IRequestFactory requestFactory)
        {
            requestFactory_ = requestFactory.Create("/{id}", new Dictionary<string, string> { { "id", Id ?? string.Empty } });
        }
    }
}