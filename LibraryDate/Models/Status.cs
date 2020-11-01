using System.ComponentModel.DataAnnotations;

namespace LibraryDate.Models
{
    public class Status
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Descriptionet { get; set; }
    }
}
