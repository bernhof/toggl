using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Toggl.Services;
using Toggl.Models;
using System.Reflection;
using System.Linq;

namespace Toggl
{
    public partial class TogglClient : IDisposable
    {
        public ClientService Clients { get; }
        public CurrentUserService CurrentUser { get; }
        public ProjectService Projects { get; }
        public TagService Tags { get; }
        public TaskService Tasks { get; }
        public WorkspaceService Workspaces { get; }

        private readonly JsonSerializer _jsonSerializer;
        private readonly HttpClient _httpClient;

        public ProductInfoHeaderValue UserAgent { get; set; }

        public TogglClient(string apiToken)
        {
            Clients = new ClientService(this);
            CurrentUser = new CurrentUserService(this);
            Projects = new ProjectService(this);
            Tags = new TagService(this);
            Tasks = new TaskService(this);
            Workspaces = new WorkspaceService(this);

            _jsonSerializer = new JsonSerializer();

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (UserAgent != null)
                _httpClient.DefaultRequestHeaders.UserAgent.Add(UserAgent);

            // authorization (basic)
            var authorizationParameter = Convert.ToBase64String(Encoding.GetEncoding("ascii")
                .GetBytes($"{apiToken}:api_token"));
            var header = new AuthenticationHeaderValue("Basic", authorizationParameter);
            _httpClient.DefaultRequestHeaders.Authorization = header;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        #region Helpers

        internal async Task<T> Get<T>(string uri, object model = null)
            => await RequestAsync<T>(HttpMethod.Get, uri, model);

        internal async Task<T> Post<T>(string uri, T model)
            => await RequestAsync<T>(HttpMethod.Post, uri, model);

        internal async Task<T> Put<T>(string uri, object model)
            => await RequestAsync<T>(HttpMethod.Put, uri, model);

        private StringContent GetJsonContent(string json)
            => new StringContent(json, Encoding.UTF8, "application/json");

        internal string GetActiveStateString(ActiveState active)
        {
            switch (active)
            {
                case ActiveState.Active: return "active";
                case ActiveState.Archived: return "archived";
                case ActiveState.Both: return "both";
                default: throw new ArgumentException("Invalid Active specification", nameof(active));
            }
        }

        protected virtual async Task<T> RequestAsync<T>(HttpMethod method, string uri, object model = null)
        {
            var request = new HttpRequestMessage(method, uri);
            if (model != null)
            {
                var requestBody = JsonConvert.SerializeObject(model);
                request.Content = GetJsonContent(requestBody);
            }
            var response = await _httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new TogglApiException(responseBody, response);
            }
            var result = JsonConvert.DeserializeObject<T>(responseBody);
            return result;
        }

        public object CreateMinimalModelForUpdate<T>(T current, T previous, out bool hasChanges, string[] skipProperties = null)
            where T : IBaseModel
        {
            var properties = typeof(T).GetTypeInfo().DeclaredProperties;
            var model = new JObject();
            hasChanges = false;
            // always include ID
            model.Add("id", current.Id);

            foreach (var prop in properties)
            {
                if (prop.Name == nameof(IBaseModel.Id)) continue; // skip Id (it's added above)
                var attributes = prop.GetCustomAttributes();
                if (attributes.OfType<JsonIgnoreAttribute>().Any()) continue; // don't check or serialize ignored attributes
                if (skipProperties != null && skipProperties.Any(p => p == prop.Name)) continue; // skip property

                var value = prop.GetValue(current);
                if (!Equals(prop.GetValue(previous), value))
                {
                    var attr = attributes.OfType<JsonPropertyAttribute>().FirstOrDefault();
                    string propertyName;
                    if (attr != null && !string.IsNullOrEmpty(attr.PropertyName))
                        propertyName = attr.PropertyName;
                    else
                        propertyName = prop.Name;

                    model.Add(propertyName, JToken.FromObject(value));
                    hasChanges = true;
                }
            }
            return model;
        }

        #endregion
    }
}
