﻿namespace PFA_ProjectAPI.Domain.Models.DtoEvent
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Creator { get; set; }
        public int Capacity { get; set; }

        public string Status { get; set; }
        public string Category { get; set; }

    }
}

