using EventService.Common.Model;

namespace EventService.Domain.Services
{
    public interface IEventCommandService
    {
        Task<EventResponse> GetEventsAsync(EventRequest eventRequest);
    }
}