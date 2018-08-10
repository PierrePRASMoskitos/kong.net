using System.Collections.Generic;
using System.Threading.Tasks;
using Kong.Interop;
using Kong.Slumber;

namespace Kong.Model
{
    public class Services : IServices
    {
        private readonly IRequestFactory requestFactory_;

        public Services(IRequestFactory requestFactory)
        {
            requestFactory_ = requestFactory;
        }

        public async Task<ResourceCollection<IService>> List()
        {
            var response = await requestFactory_.List<ResourceCollection<Service>>(null).ConfigureAwait(false);

            var serviceCollection = new ResourceCollection<IService>();

            foreach (var service in response.Data)
            {
                service.Configure(requestFactory_);
                serviceCollection.Data.Add(service);
            }

            return serviceCollection;
        }

        public async Task<IService> Create(IService service)
        {
            service.Configure(requestFactory_);
            return await service.Create();
        }

        public async Task<IService> Update(IService service)
        {
            service.Configure(requestFactory_);
            return await service.Update();
        }

        public async Task<IService> Get(IService service)
        {
            service.Configure(requestFactory_);
            return await service.Get();
        }
    }
}