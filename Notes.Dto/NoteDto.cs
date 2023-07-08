using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Dto
{
    public class NoteDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        public string Content { get; set; }
        
        public bool isDeleted { get; set; }
    }
}
