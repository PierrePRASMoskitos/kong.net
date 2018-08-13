using Kong.Slumber;

namespace Kong.Interop
{
    public interface IPluginConfiguration
    {
        void Configure(IRequestFactory requestFactory);
    }
}