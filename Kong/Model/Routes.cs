using System.Threading.Tasks;
using Kong.Interop;
using Kong.Slumber;

namespace Kong.Model
{
    public class Routes : IRoutes
    {
        private readonly IRequestFactory requestFactory_;

        public Routes(IRequestFactory requestFactory)
        {
            requestFactory_ = requestFactory;
        }

        public async Task<ResourceCollection<IRoute>> List()
        {
            var response = await requestFactory_.List<ResourceCollection<Route>>(null).ConfigureAwait(false);

            var routeCollection = new ResourceCollection<IRoute>();

            foreach (var route in response.Data)
            {
                route.Configure(requestFactory_);
                routeCollection.Data.Add(route);
            }

            return routeCollection;
        }

        public async Task<IRoute> Create(IRoute route)
        {
            route.Configure(requestFactory_);
            return await route.Create<Route>();
        }

        public async Task<IRoute> Update(IRoute route)
        {
            route.Configure(requestFactory_);
            return await route.Update<Route>();
        }

        public async Task<IRoute> Get(IRoute route)
        {
            route.Configure(requestFactory_);
            return await route.Get<Route>();
        }
    }
}