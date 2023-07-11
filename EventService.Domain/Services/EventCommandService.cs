using EventService.Common.Constants;
using EventService.Common.Model;
using EventService.Provider.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net;

namespace EventService.Domain.Services
{
    public class EventCommandService : IEventCommandService
    {
        private readonly IEventRepository _eventRepository;
        private readonly AppConfiguration _appConfiguration;
        private readonly ILogger<EventCommandService> _logger;
        public EventCommandService(IEventRepository eventRepository, AppConfiguration appConfiguration, ILogger<EventCommandService> logger)
        {
            _eventRepository = eventRepository;
            _appConfiguration = appConfiguration;
            _logger = logger;
        }

        public async Task<EventResponse> GetEventsAsync(EventRequest eventRequest)
        {
            EventResponse response = new();
            
            //Checking if the email id in the request is valid or not.
            if (!_appConfiguration.ValidEmailIds.Contains(eventRequest.Email.ToLowerInvariant()))
            {
                response.Title = CommonConstants.COMMON_FAILURE;
                response.StatusMessage = CommonConstants.INV_EMAIL_ID;
                _logger.LogCritical(string.Format(CommonConstants.LOG_MSG_CRI_INV_EMAIL, eventRequest.Email));
                return response;
            }

            //Fetching response from API
            string apiResponse = string.Empty;
            try
            {
                //Making a call to the API
                (apiResponse, response.StatusCode) = await _eventRepository.GetEventsAsync(eventRequest.Email).ConfigureAwait(false);
                var jsonObject = JObject.Parse(apiResponse);

                //If the API returns the desired output
                if (response.StatusCode == HttpStatusCode.OK.ToString())
                {
                    var events = jsonObject[CommonConstants.RESP_PARAM_EVENTS].ToObject<List<Event>>();
                    var filteredEvents = events
                        .Where(e => (e.status == CommonConstants.STATUS_BUSY || e.status == CommonConstants.STATUS_OUT_OF_OFFICE) && e.start > DateTime.Now)
                        .ToList();
                    response.Events = filteredEvents;
                    response.Email = jsonObject[CommonConstants.RESP_PARAM_EMAIL].ToString();
                    response.Number_of_events = Int32.Parse(jsonObject[CommonConstants.RESP_PARAM_NO_OF_EVENTS].ToString());
                    response.Title = CommonConstants.COMMON_SUCCESS;
                    response.StatusMessage = CommonConstants.FETCH_SUCCESS;
                }
                //If there is an issue in the response
                else
                {
                    response.Title = CommonConstants.COMMON_FAILURE;
                    response.StatusMessage = jsonObject.ContainsKey(CommonConstants.RESP_PARAM_MESSAGE) ? jsonObject[CommonConstants.RESP_PARAM_MESSAGE].ToString() : null;
                    response.StatusCode = response.StatusCode;
                    _logger.LogError(CommonConstants.LOG_MSG_ERR_FAILURE);
                }
            }
            //If an unexpected exception happens due to any reason.
            catch (Exception ex)
            {
                response.Title = CommonConstants.COMMON_EXCEPTION;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
    }
}
