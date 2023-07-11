namespace EventService.Common.Model
{
    public class EventResponse : BaseApiResponse
    {
        public string Email { get; set; }
        public int? Number_of_events { get; set; }
        public List<Event> Events { get; set; }
    }
}
