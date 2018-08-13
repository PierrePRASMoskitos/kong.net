using System.Threading.Tasks;
using Kong.Interop;
using Kong.Model;
using Kong.Slumber;

namespace Kong.Client
{
    public class KongClient : IKongClient
    {
        private readonly IRequestFactory requestFactory_;

        internal KongClient(IRequestFactory requestfactory)
        {
            requestFactory_ = requestfactory;
        }

        public async Task<Node> Node()
        {
            var requestFactory = requestFactory_.Create("/");
            var response = await requestFactory.Get<Node>().ConfigureAwait(false);
            return response;
        }

        public async Task<Status> Status()
        {
            var requestFactory = requestFactory_.Create("/status");
            var response = await requestFactory.Get<Status>().ConfigureAwait(false);
            return response;
        }

        public async Task<ICluster> Cluster()
        {
            var requestFactory = requestFactory_.Create("/cluster");
            var response = await requestFactory.Get<Cluster>().ConfigureAwait(false);
            response.Configure(requestFactory);
            return response;
        }

        public IRequestFactory RequestFactory => requestFactory_;

        public IApis Apis => new Apis(requestFactory_.Create("/apis"));
        
        public IConsumers Consumers => new Consumers(requestFactory_.Create("/consumers"));
        
        public IKongEntity Route => new Route(requestFactory_.Create("/routes"));

        public IKongEntity Service => new Service(requestFactory_.Create("/services"));
    }
}
