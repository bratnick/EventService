using EventService.Common.Constants;
using EventService.Common.Exceptions;
using EventService.Common.Model;
//using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using System.Text;
using static System.Net.WebRequestMethods;

namespace EventService.Provider.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy<(string, string)> _retryPolicy;
        private readonly AppConfiguration _appConfiguration;
        public EventRepository(HttpClient httpClient, AppConfiguration appConfiguration)
        {
            _httpClient = httpClient;
            _appConfiguration = appConfiguration;
            // Define the retry policy
            _retryPolicy = Policy<(string, string)>
                .Handle<Exception>() // Specify the exception(s) to handle
                .WaitAndRetryAsync(appConfiguration.RetryCount, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))); // Retry 3 times with exponential backoff
        }

        //This method is where the API mentioned in the problem statement is called.
        //I have implemented Polly retry here with a standard 2^n wait time where n is configured in appsettings.
        public async Task<(string, string)> GetEventsAsync(string emailId)
        {
            var url = _appConfiguration.EventURL + emailId;
            var events = await _retryPolicy.ExecuteAsync(async () =>
            {
                var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return (content, response.StatusCode.ToString());
                }
                else
                {
                    // Handle non-successful response
                    return (string.Empty, response.StatusCode.ToString());
                }
            });
            return events;
            //try
            //{
            //    var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            //    var content = await response.Content.ReadAsStringAsync();
            //    return (content, response.StatusCode.ToString());
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}
