using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Humanizer;

namespace MessageProvider.Models
{
    public class JobViewModel
    {
        public IEnumerable<string> Teams { get; set; }


        [DisplayName("Team")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kies het team")]
        public string Team { get; set; } = string.Empty;

        [DisplayName("Berichttype")]
        [ReadOnly(true)]
        public string MessageType { get; set; } = string.Empty;

        [DisplayName("Aantal berichten")]
        [Required(ErrorMessage = "Voer een aantal in tussen 1 en 10 inclusief.")]
        [Range(1, 10, ErrorMessage = "Waarde tussen 1 en 10 inclusief")]
        public int MessageCount { get; set; } = 1;

        [DisplayName("Aanvrager id")]
        [StringLength(32)]
        public string? SubjectId { get; set; }

        public JobViewModel()
        {
            var list = new List<string>();
            for (var i = 0; i < 20; i++)
            {
                list.Add($"team-{i+1:D2}");
            }

            Teams = list.ToArray();
        }
    }
}
