using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MessageProvider.Models
{
    public class JobViewModel
    {
        [DisplayName("Berichttype")]
        [ReadOnly(true)]
        public string MessageType { get; set; } = string.Empty;

        [DisplayName("Aantal berichten")]
        [Required(ErrorMessage = "Voer een aantal in tussen 1 en 10 inclusief.")]
        [Range(1, 10, ErrorMessage = "Waarde tussen 1 en 10 inclusief")]
        public int MessageCount { get; set; } = 1;

        [DisplayName("Aanvrager id")]
        [StringLength(36)]
        public string? SubjectId { get; set; }
    }
}
