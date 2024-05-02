namespace PFA_ProjectAPI.Models.EventModels
{
    public class UpdateEvent
    {

        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; } = string.Empty;
        public string Participants { get; set; }
        public string Creator { get; set; }
        public int Capacity { get; set; }

        public string Activities { get; set; }

        public string Status { get; set; }

        public string Category { get; set; }
    }
}
