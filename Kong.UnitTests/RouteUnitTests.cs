using System.Threading.Tasks;
using Kong.Factory;
using Kong.Interop;
using Kong.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kong.UnitTests
{
    [TestClass]
    public class RouteUnitTests
    {
        [TestMethod]
        public async Task Test()
        {
            // Context

            var factory = new KongClientFactory("http://localhost:8001");
            var client = factory.Create();                    

            var service = new ServiceData()
            {
                Name = "PetStore",
                Host = "petstore.swagger.io",
                Path = "/v2",
                Protocol = "http",
            };
            
            // System under test

            var createdService = await client.Service.CreateAsync(service);

            var routeData = new RouteData()
            {
                Hosts = new[] {"petstore.swagger.io"},
                Paths = new []{"/petstore"},
                ServiceInformation = new ServiceInformation()
                {
                    ServiceId = createdService.Id,
                },
            };

            // Assert - Expectations

            var createdRoute = await client.Route.CreateAsync(routeData);

            var services = await client.Service.List<ServiceData>();
            var routes = await client.Route.List<RouteData>();
            var route = await client.Route.GetEntityAsync<RouteData>(createdRoute.Id);

            Assert.IsNotNull(services);
            Assert.IsNotNull(routes);
            Assert.IsNotNull(route);
        }
    }
}
