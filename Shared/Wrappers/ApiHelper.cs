using Microsoft.AspNetCore.WebUtilities;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Shared.Data;

namespace Shared.Wrappers
{
    public sealed class ApiHelper
    {
        #region [Static]

        #region Properties

        public static ApiHelper Instance { get; private set; } = new();

        #endregion

        #region Get Method Url

        public static string GetMethodUrl(string baseAddress, string? controllerAddress, string methodName, IDictionary<string, string?>? queryString = null) 
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
                throw new ArgumentException($"Invalid value: ({baseAddress})", nameof(baseAddress));

            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentException($"Invalid value: ({methodName})", nameof(methodName));

            string url = $"{baseAddress}{(!string.IsNullOrWhiteSpace(controllerAddress) ? $"/{controllerAddress}" : string.Empty)}/{methodName}";

            if (queryString?.Count > 0)
                url = QueryHelpers.AddQueryString(url, queryString);

            return url;
        }

        #endregion

        #region Create Request

        public static HttpRequestMessage CreateRequest(string url, HttpMethod httpMethod, object? content = null)
        {
            HttpRequestMessage request = new(httpMethod, url);

            if (content != null)
            {
                string jsonContent = JsonSerializer.Serialize(content);

                request.Content = new StringContent(jsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            }

            return request;
        }

        public static HttpRequestMessage CreateGetRequest(string url, object? content = null) =>
            CreateRequest(url, HttpMethod.Get, content);

        public static HttpRequestMessage CreatePostRequest(string url, object? content = null) =>
            CreateRequest(url, HttpMethod.Post, content);

        public static HttpRequestMessage CreatePutRequest(string url, object? content = null) =>
            CreateRequest(url, HttpMethod.Put, content);

        public static HttpRequestMessage CreateDeleteRequest(string url, object? content = null) =>
            CreateRequest(url, HttpMethod.Delete, content);

        #endregion

        #region Send Request

        public static async Task<ApiResponse?> SendRequestAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null)
        {
            using HttpClient client = new();

            client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

            using HttpResponseMessage response = cancellationToken == null ?
                await client.SendAsync(request, HttpCompletionOption.ResponseContentRead) :
                await client.SendAsync(request, HttpCompletionOption.ResponseContentRead, (CancellationToken)cancellationToken);

            using Stream contentSream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<ApiResponse>(contentSream);
        }

        public static async Task<T?> SendRequestAsync<T>(HttpRequestMessage request, CancellationToken? cancellationToken = null)
        {
            ApiResponse? response = await SendRequestAsync(request, cancellationToken);

            if (response == null)
                throw new NullReferenceException(nameof(response));

            if (response.Success)
            {
                if (response.Result == null)
                    throw new NullReferenceException(nameof(response.Result));

#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                return JsonSerializer.Deserialize<T>(response.Result.ToString());
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            }

            throw new Exception(response.ToString());
        }

        #endregion

        #endregion

        #region [Instance]

        #region Properties

        public string BaseAddress { get; set; } = string.Empty;

        public string ControllerAddress { get; set; } = string.Empty;

        #endregion

        #region Get Method Url

        public string GetMethodUrl(string methodName, IDictionary<string, string?>? queryString = null) =>
            GetMethodUrl(BaseAddress, ControllerAddress, methodName, queryString);

        #endregion

        #region Call API Method

        public async Task<ApiResponse?> CallMethodAsync(string methodName, HttpMethod httpMethod, object? content = null, IDictionary<string, string?>? parameters = null, CancellationToken? cancellationToken = null)
        {
            var url = GetMethodUrl(methodName, parameters);

            var request = CreateRequest(url, httpMethod, content);

            return await SendRequestAsync(request, cancellationToken);
        }

        public async Task<T?> CallMethodAsync<T>(string methodName, HttpMethod httpMethod, object? content = null, IDictionary<string, string?>? parameters = null, CancellationToken? cancellationToken = null)
        {
            var url = GetMethodUrl(methodName, parameters);

            var request = CreateRequest(url, httpMethod, content);

            return await SendRequestAsync<T>(request, cancellationToken);
        }

        #endregion

        #region Call API Method via specified HTTP Method

        public async Task<ApiResponse?> CallGetMethodAsync(string methodName, object? content = null, IDictionary<string, string?>? parameters = null, CancellationToken? cancellationToken = null) =>
            await CallMethodAsync(methodName, HttpMethod.Get, content, parameters, cancellationToken);

        public async Task<T?> CallGetMethodAsync<T>(string methodName, object? content = null, IDictionary<string, string?>? parameters = null, CancellationToken? cancellationToken = null) =>
            await CallMethodAsync<T>(methodName, HttpMethod.Get, content, parameters, cancellationToken);

        public async Task<ApiResponse?> CallPostMethodAsync(string methodName, object? content = null, IDictionary<string, string?>? parameters = null, CancellationToken? cancellationToken = null) =>
            await CallMethodAsync(methodName, HttpMethod.Post, content, parameters, cancellationToken);

        public async Task<T?> CallPostMethodAsync<T>(string methodName, object? content = null, IDictionary<string, string?>? parameters = null, CancellationToken? cancellationToken = null) =>
            await CallMethodAsync<T>(methodName, HttpMethod.Post, content, parameters, cancellationToken);

        public async Task<ApiResponse?> CallPutMethodAsync(string methodName, object? content = null, IDictionary<string, string?>? parameters = null, CancellationToken? cancellationToken = null) =>
            await CallMethodAsync(methodName, HttpMethod.Put, content, parameters, cancellationToken);

        public async Task<T?> CallPutMethodAsync<T>(string methodName, object? content = null, IDictionary<string, string?>? parameters = null, CancellationToken? cancellationToken = null) =>
            await CallMethodAsync<T>(methodName, HttpMethod.Put, content, parameters, cancellationToken);

        public async Task<ApiResponse?> CallDeleteMethodAsync(string methodName, object? content = null, IDictionary<string, string?>? parameters = null, CancellationToken? cancellationToken = null) =>
            await CallMethodAsync(methodName, HttpMethod.Delete, content, parameters, cancellationToken);

        public async Task<T?> CallDeleteMethodAsync<T>(string methodName, object? content = null, IDictionary<string, string?>? parameters = null, CancellationToken? cancellationToken = null) =>
            await CallMethodAsync<T>(methodName, HttpMethod.Delete, content, parameters, cancellationToken);

        #endregion

        #endregion
    }
}
