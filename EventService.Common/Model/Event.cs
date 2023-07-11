namespace EventService.Common.Model
{
    public class Event
    {
        public string subject { get; set; }
        public string status { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string id { get; set; }
    }
}
