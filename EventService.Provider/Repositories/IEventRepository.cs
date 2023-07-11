using EventService.Common.Model;

namespace EventService.Provider.Repositories
{
    public interface IEventRepository
    {
        Task<(string, string)> GetEventsAsync(string emailId);
    }
}