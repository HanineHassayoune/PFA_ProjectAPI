using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace PFA_ProjectAPI.Domain.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped] // To not store in database
        public IFormFile File { get; set; } // The type of file 

        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FilePath { get; set; }

        public Guid? ActivityId { get; set; }
        public Activity Activity { get; set; }

        // Foreign key for many-to-one relationship with Event
        public Guid? EventId { get; set; }
        public Event Event { get; set; }

    }
}