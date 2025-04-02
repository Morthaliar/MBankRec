using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Przelew
    {
        public Guid Id { get; set; }

        [Required]
        public float LP { get; set; }

        [Required]
        public int Kwota { get; set; }

        [Required]
        public string RachunekNadawcy { get; set; }

        [Required]
        public string RachunekOdbiorcy { get; set; }

        [MaxLength(150)]
        public string? Opis { get; set; }

        public DateTime? DodanyDnia { get; set; }

        public TrybDodania? TrybDodania { get; set; }

        public string PlikPochodzenia { get; set; }
    }
}
