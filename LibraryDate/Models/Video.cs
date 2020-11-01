using System.ComponentModel.DataAnnotations;

namespace LibraryDate.Models
{
    public class Video : LibraryAsset
    {
        [Required]
        public string Director { get; set; }
    }
}
