using System.Threading.Tasks;
using Kong.Slumber;

namespace Kong.Interop
{
    public interface IService
    {
        string Id { get; set; }
        long CreatedAt { get; set; }
        long UpdatedAt { get; set; }
        int ConnectTimeout { get; set; }
        string Protocol { get; set; }
        string Host { get; set; }
        int Port { get; set; }
        string Path { get; set; }
        string Name { get; set; }
        int Retries { get; set; }
        int ReadTimeout { get; set; }
        int WriteTimeout { get; set; }
        IPlugins Plugins { get; }

        Task<IService> Create();
        Task<IService> Update();
        Task<IService> Get();
        Task Delete();

        void Configure(IRequestFactory requestFactory);
    }
}