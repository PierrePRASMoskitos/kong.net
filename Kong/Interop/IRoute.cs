using Kong.Slumber;

namespace Kong.Interop
{
    public interface IRoute : IEntityOperations
    {
        string Id { get; set; }
        long CreatedAt { get; set; }
        long UpdatedAt { get; set; }
        string[] Protocols { get; set; }
        string[] Methods { get; set; }
        string[] Hosts { get; set; }
        string[] Paths { get; set; }
        int RegexPriority { get; set; }
        bool StripPath { get; set; }
        bool PreserveHost { get; set; }
        IService Service { get; set; }
        IPlugins Plugins { get; }

        void Configure(IRequestFactory requestFactory);
    }
}