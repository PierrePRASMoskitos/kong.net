using System;
using Kong.Interop;
using Kong.Serialization;
using Kong.Slumber;
using Newtonsoft.Json;
using Slumber;
using Slumber.Http;
using Slumber.Json;
using Slumber.Logging;

namespace Kong
{
    public class KongClientFactory : IKongClientFactory
    {
        private readonly string _url;
        private readonly bool _debug;
        private ISlumberConfiguration configuration_;

        public KongClientFactory(string url, bool debug = true)
        {
            _url = url;
            _debug = debug;
        }

        public IKongClient Create()
        {
            var client = new SlumberClient(SlumberConfigurationFactory.Empty(_url, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1), Configure));
            var requestFactory = new RequestFactory(client);
            return new KongClient(requestFactory);
        }

        private void Configure(ISlumberConfiguration configuration)
        {
            configuration.UseJsonSerialization(Customise).UseHttp(http => Customise(http, configuration));
            if (!_debug)
            {
                return;
            }
            configuration.UseConsoleLogger();
            configuration_ = configuration;
        }

        private void Customise(JsonSerializerSettings settings)
        {
            settings.ContractResolver = new SnakeCaseContractResolver();
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        public ISlumberConfiguration Confguration => configuration_;

        private void Customise(Http http, ISlumberConfiguration configuration)
        {
            http.UseJsonAsDefaultContentType();
        }
    }
}
