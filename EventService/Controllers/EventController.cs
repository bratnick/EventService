using EventService.Common.Constants;
using EventService.Common.Model;
using EventService.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventService.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {

        private readonly ILogger<EventController> _logger;
        private readonly IEventCommandService _eventService;

        public EventController(ILogger<EventController> logger, IEventCommandService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }

        public async Task<ActionResult<EventResponse>> GetEvents(EventRequest eventRequest)
        {
            try
            {
                _logger.LogInformation(CommonConstants.LOG_MSG_INFO_STARTED, eventRequest.Email);
                var eventResponse = await _eventService.GetEventsAsync(eventRequest).ConfigureAwait(false);
                return Ok(eventResponse);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                _logger.LogCritical(ex.Message);
                return StatusCode(500, $"Error retrieving events: {ex.Message}");
            }
        }
    }
}