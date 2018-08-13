namespace Kong.Model
{
    public class ServiceData
    {
        public string Id { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int ConnectTimeout { get; set; }
        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int Retries { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }
    }
}