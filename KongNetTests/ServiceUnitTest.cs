using System.Threading.Tasks;
using Kong;
using Kong.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KongNetTests
{
    [TestClass]
    public class ServiceUnitTest
    {
        [TestMethod]
        public async Task TestKongClient()
        {
            // Context 

            var factory = new KongClientFactory("http://localhost:8001");
            var client = factory.Create();

            //var service = new Service
            //{
            //    Name = "PetstoreTest",
            //    Host = "petstore.swagger.io",
            //    Path = "/v2",
            //    Protocol = "http"
            //};

            //// System under test

            //var createdService = await client.Services.Create(service);

            var route = new Route()
            {
                Hosts = new[] { "blabla" },
                Paths = new[] { "/path" },
                Service = new Service() { Id = "d17ca507-31ad-4d89-aa73-e55df66cc391" }
            };

            var createdRoute = await client.Routes.Create(route);

            // Assert - Expectations

        }
    }
}
