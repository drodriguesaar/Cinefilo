using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Net.Http.Headers;

namespace CinefiloWASM.Services
{
    internal class ServiceBase
    {
        protected HttpClient _baseHttpClient { get; private set; }
        protected string EndPointDomain { get; private set; }
        protected string EndPointAPIKey { get; private set; }
        protected string EndPoint { get; set; }

        public ServiceBase(string endPointDomain, string endPointAPIKey)
        {
            this.EndPointAPIKey = endPointAPIKey;
            this.EndPointDomain = endPointDomain;
            SetBaseHttpClient();
        }

        void SetBaseHttpClient()
        {
            var httpClientHandler = new HttpClientHandler
            {
            };
            _baseHttpClient = new HttpClient(httpClientHandler, false)
            {
                BaseAddress = new Uri(EndPointDomain),

            };

            _baseHttpClient.DefaultRequestHeaders.Clear();
            _baseHttpClient.DefaultRequestHeaders.Accept.Clear();
            _baseHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        protected async Task<TResult> Get<TData, TResult>(TData data)
        {
            var queryString = GenerateQueryString(data);

            Console.WriteLine(queryString);

            var resposta = await _baseHttpClient.GetFromJsonAsync<TResult>(string.Format("{0}&{1}", EndPoint, queryString));

            

            return resposta;
        }
        private string GenerateQueryString(object param)
        {
            var paramType = param.GetType();
            var paramTypeProperties = paramType.GetProperties();

            var querystring = string.Empty;
            var result = new Dictionary<string, object>();

            result = ExtractProperties(param, result);

            result = result.Where(r => r.Value != null)
                           .ToList()
                           .ToDictionary(x => x.Key, x => x.Value);

            querystring = BuildQueryString(result);

            return querystring;
        }

        private string BuildQueryString(Dictionary<string, object> parameterCollection)
        {
            var keyValueStrings =
                    parameterCollection
                    .Select(pair => string.Format("{0}={1}", pair.Key, pair.Value));

            return string.Join("&", keyValueStrings).ToLower();
        }

        private Dictionary<string, object> ExtractProperties(object param, Dictionary<string, object> result)
        {
            result = param.GetType()
                          .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                          .ToDictionary(prop => prop.Name, prop => prop.GetValue(param, null));

            return result;
        }
    }
}