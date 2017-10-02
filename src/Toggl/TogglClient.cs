using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toggl.Services;
using Toggl.Throttling;


namespace Toggl
{
    /// <summary>
    /// Communicates with the Toggl API
    /// </summary>
    public partial class TogglClient : IDisposable
    {
        /// <summary>
        /// Creates a throttler that is guaranteed to not exceed rate limits as per Toggl API documentation.
        /// (https://github.com/toggl/toggl_api_docs#the-api-format)
        /// </summary>
        /// <returns>A <see cref="LeakyBucketThrottler"/> that allows one request per second.</returns>
        /// <remarks>
        /// Note that guarantee only holds for a single instance of <see cref="LeakyBucketThrottler"/> with the assumption
        /// that leaky bucket is empty at the time of performing the first request.
        /// </remarks>
        public static LeakyBucketThrottler CreateSafeThrottler()
        {
            return new LeakyBucketThrottler(1, TimeSpan.FromSeconds(1));
        }

        #region Services

        /// <summary>
        /// Client service
        /// </summary>
        public ClientService Clients { get; }
        /// <summary>
        /// Time entry service
        /// </summary>
        public TimeEntryService TimeEntries { get; }
        /// <summary>
        /// Project service
        /// </summary>
        public ProjectService Projects { get; }
        /// <summary>
        /// Tag service
        /// </summary>
        public TagService Tags { get; }
        /// <summary>
        /// Task service
        /// </summary>
        public TaskService Tasks { get; }
        /// <summary>
        /// Workspace service
        /// </summary>
        public WorkspaceService Workspaces { get; }

        #endregion

        const string _baseUrl = "https://www.toggl.com/api/v9/";

        private readonly JsonSerializer _jsonSerializer;
        private readonly HttpClient _httpClient;
        private readonly IThrottler _throttler;

        /// <summary>
        /// User-Agent header information to include in requests. Default is null.
        /// If null, User-Agent header will not be included in requests.
        /// </summary>
        public ProductInfoHeaderValue UserAgent { get; set; }

        /// <summary>
        /// Creates new <see cref="TogglClient"/>
        /// </summary>
        /// <param name="apiToken">Toggl API token</param>
        /// <param name="throttler">Throttler to use when invoking methods. If null, no throttling is performed</param>
        public TogglClient(string apiToken, IThrottler throttler = null)
        {
            Clients = new ClientService(this);
            TimeEntries = new TimeEntryService(this);
            Projects = new ProjectService(this);
            Tags = new TagService(this);
            Tasks = new TaskService(this);
            Workspaces = new WorkspaceService(this);

            _jsonSerializer = JsonSerializer.CreateDefault();
            _throttler = throttler ?? new NeutralThrottler();

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (UserAgent != null)
                _httpClient.DefaultRequestHeaders.UserAgent.Add(UserAgent);

            // authorization (basic)
            var authorizationParameter = Convert.ToBase64String(Encoding.GetEncoding("ascii").GetBytes($"{apiToken}:api_token"));
            var header = new AuthenticationHeaderValue("Basic", authorizationParameter);
            _httpClient.DefaultRequestHeaders.Authorization = header;
        }

        /// <summary>
        /// Releases all resources.
        /// </summary>
        public void Dispose()
        {
            _httpClient.Dispose();
        }

        #region Helpers

        internal Task<T> Get<T>(string uri, CancellationToken cancellationToken, object model = null)
            => RequestAsync<T>(HttpMethod.Get, uri, model);

        internal Task<T> Post<T>(string uri, CancellationToken cancellationToken, T model)
            => RequestAsync<T>(HttpMethod.Post, uri, model, cancellationToken);

        internal Task<T> Put<T>(string uri, CancellationToken cancellationToken, object model)
            => RequestAsync<T>(HttpMethod.Put, uri, model);

        private StringContent GetJsonContent(string json)
            => new StringContent(json, Encoding.UTF8, "application/json");

        /// <summary>
        /// Reads through all or specific pages returned by a given query.
        /// </summary>
        /// <typeparam name="T">Type of item returned by query</typeparam>
        /// <param name="pagedQuery">Query delegate function</param>
        /// <param name="fromPage">First page to read</param>
        /// <param name="toPage">Last page to read (inclusive)</param>
        /// <returns>All items in the specified page range</returns>
        /// <example>
        /// <code>
        /// var result = await togglClient.GetPages(page => togglClient.Tasks.ListAsync(workspaceId: 1234567, page: page));
        /// </code>
        /// </example>
        public virtual IObservable<T> GetPages<T>(
            Func<int, Task<Models.PagedResult<T>>> pagedQuery,
            int fromPage = 1,
            int toPage = int.MaxValue)
        {
            // Preconditions
            if (pagedQuery == null) throw new ArgumentNullException(nameof(pagedQuery));
            Utilities.CheckPageArgument(fromPage);
            Utilities.CheckPageArgument(toPage);
            if (toPage < fromPage) throw new ArgumentException($"Invalid page range specified: {nameof(toPage)} must be larger than or equal to {nameof(fromPage)}", nameof(toPage));

            var iterator = new Func<IObserver<T>, Task>(async observer =>
            {
                // Request specified pages
                bool more;
                int page = fromPage;

                do
                {
                    var result = await pagedQuery(page);
                    if (result.Items != null)
                    {
                        foreach (var item in result.Items)
                            observer.OnNext(item);
                    }
                    // See if there are more items to retrieve
                    more = result.TotalCount > result.Page * result.ItemsPerPage;
                    page++;
                }
                while (more && page <= toPage);

                observer.OnCompleted();
            });

            return Observable.Create(iterator);
        }

        internal virtual async Task<T> RequestAsync<T>(HttpMethod method, string uri, object model = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = new HttpRequestMessage(method, uri);
            if (model != null)
            {
                var requestBody = JsonConvert.SerializeObject(model);
                request.Content = GetJsonContent(requestBody);
            }

            var response = await _throttler.ThrottleAsync(
                () => _httpClient.SendAsync(request, cancellationToken), 
                cancellationToken);

            var responseBody = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new TogglApiException(responseBody, response);
            }
            var result = JsonConvert.DeserializeObject<T>(responseBody);
            return result;
        }

        #endregion
    }
}
