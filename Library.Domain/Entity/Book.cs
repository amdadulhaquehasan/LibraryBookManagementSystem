using Library.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entity
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 150 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 13 characters.")]
        public string ISBN { get; set; }

        [Range(1500, 2100, ErrorMessage = "Publication year must be between 1500 and 2100.")]
        public int PublicationYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pages must be a positive number.")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        public Genre Genre { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }
    }
}
