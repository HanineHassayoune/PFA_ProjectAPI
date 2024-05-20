namespace PFA_ProjectAPI.Models.Domain
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; } = string.Empty;
        public string Participants { get; set; }
        public string Creator { get; set; }
        public int Capacity { get; set; }
        public string Activities { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }

        //public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
