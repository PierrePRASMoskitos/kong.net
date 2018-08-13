using System.Collections.Generic;
using System.Threading.Tasks;
using Kong.Exceptions;
using Slumber;

namespace Kong.Slumber
{
    internal sealed class RequestFactory : IRequestFactory
    {
        private readonly string baseUrl_;
        private readonly IDictionary<string, string> parameters_;
        private readonly ISlumberClient client_;

        public RequestFactory(ISlumberClient client)
            : this(null, client, string.Empty, new Dictionary<string, string>())
        {

        }

        public RequestFactory(IRequestFactory parent, ISlumberClient client, string baseUrl, IDictionary<string, string> parameters)
        {
            Parent = parent;
            baseUrl_ = baseUrl;
            parameters_ = parameters;
            client_ = client;
        }

        public async Task<T> List<T>(IDictionary<string, object> parameters)
        {
            var request = HttpRequestBuilder<T>
                .Get(baseUrl_)
                .QueryParameters(parameters_)
                .QueryParameters(parameters, true)
                .Build();
            var response = await Execute(request).ConfigureAwait(false);
            return GetContent(response);
        }

        public async Task<T> Post<T>(object data, IDictionary<string, string> headers = null)
        {
            var request = HttpRequestBuilder<T>.Post(baseUrl_).QueryParameters(parameters_).Content(data).Headers(headers).Build();
            var response = await Execute(request).ConfigureAwait(false);
            return GetContent(response);
        }

        public async Task<T> Post<T>(IDictionary<string, string> headers = null)
        {
            var request = HttpRequestBuilder<T>
                .Post(baseUrl_)
                .QueryParameters(parameters_)
                .Build();
            var response = await Execute(request).ConfigureAwait(false);
            return GetContent(response);
        }

        public async Task<T> Get<T>(string id)
        {
            var request = HttpRequestBuilder<T>
                .Get($"{baseUrl_}/{{id}}")
                .QueryParameters(parameters_)
                .QueryParameter("id", id)
                .Build();
            var response = await Execute(request).ConfigureAwait(false);
            return GetContent(response);
        }

        public async Task<T> Get<T>()
        {
            var request = HttpRequestBuilder<T>
                .Get($"{baseUrl_}")
                .QueryParameters(parameters_)
                .Build();
            var response = await Execute(request).ConfigureAwait(false);
            return GetContent(response);
        }

        public async Task<T> Put<T>(object data)
        {
            var request = HttpRequestBuilder<T>
                .Put($"{baseUrl_}")
                .QueryParameters(parameters_)
                .Content(data)
                .Build();
            var response = await Execute(request).ConfigureAwait(false);
            return GetContent(response);
        }

        public async Task<T> Patch<T>(object data)
        {
            var request = HttpRequestBuilder<T>
                .Patch($"{baseUrl_}")
                .QueryParameters(parameters_)
                .Content(data)
                .Build();
            var response = await Execute(request).ConfigureAwait(false);
            return GetContent(response);
        }

        public Task Delete()
        {
            var request = HttpRequestBuilder<dynamic>
                .Delete($"{baseUrl_}")
                .QueryParameters(parameters_)
                .Build();
            return Execute(request);
        }

        public IRequestFactory Create(string url)
        {
            return Create(url, new Dictionary<string, string>());
        }

        public IRequestFactory Create(IDictionary<string, string> parameters)
        {
            return new RequestFactory(this, client_, baseUrl_, Merge(parameters));
        }

        public IRequestFactory Create(string url, IDictionary<string, string> parameters)
        {
            return new RequestFactory(this, client_, Merge(url), Merge(parameters));
        }

        public IRequestFactory Parent { get; }

        public IRequestFactory Root
        {
            get
            {
                if (Parent == null)
                {
                    return this;
                }

                return Parent.Root;
            }
        }

        private string Merge(string url)
        {
            return string.Join(url.StartsWith("/") ? string.Empty : "/", baseUrl_, url);
        }

        private IDictionary<string, string> Merge(IDictionary<string, string> parameters)
        {
            foreach (var parameter in parameters_)
            {
                if (parameters.ContainsKey(parameter.Key))
                {
                    continue;
                }
                parameters.Add(parameter.Key, parameter.Value);
            }
            return parameters;
        }

        private T GetContent<T>(IResponse<T> response)
        {
            return string.IsNullOrEmpty(response.Content) ? default(T) : response.Data;
        }

        private async Task<IResponse<T>> Execute<T>(IRequest<T> request)
        {
            var response = await client_.ExecuteAsync(request).ConfigureAwait(false);
            HandleError(response);
            return response;
        }

        public void HandleError(IResponse response)
        {
            if (!response.HasError)
            {
                return;
            }
            var content = response.Exception.GetContent<dynamic>(client_.Configuration.Serialization.CreateDeserializer(response));
            throw new ApiException(response.StatusCode, content, response.Exception);
        }
    }
}