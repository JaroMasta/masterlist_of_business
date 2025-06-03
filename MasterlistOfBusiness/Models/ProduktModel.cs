using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MasterlistOfBusiness.Models
{
    public class Produkt
    {
        [Key]
        [Display(Name = "Numer id produktu")]
        public int id_produktu { get; set; }

        public string? nazwa { get; set;  }

        [Required]
        [Display(Name = "Typ produktu")]
        [StringLength(100, ErrorMessage = "Typ produktu nie może przekraczać 100 znaków.")]
        public string typ { get; set; }

        [Display(Name = "Opis produktu")]
        [StringLength(500, ErrorMessage = "Opis produktu nie może przekraczać 500 znaków.")]
        public string? opis { get; set; }

        [Display(Name = "Cena")]
        [Range(1, int.MaxValue, ErrorMessage = "Cena musi być wartoscia calkowita wieksza niz 0.")]
        public int cena { get; set; }

        [Display(Name = "Ilosc")]
        [Range(0, int.MaxValue, ErrorMessage = "Ilosc musi byc wartoscia calkowita wieksza lub rowna 0.")]
        public int ilosc { get; set; } = 0;

        public virtual int id_konta { get; set; }
        [Display(Name = "Konto")]
        
        public virtual Konto Konto { get; set; }

        [Display(Name = "Transakcje")]
        public virtual ICollection<Transakcja>? Transakcje { get; set; } = new List<Transakcja>();
    }
}