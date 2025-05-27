using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MasterlistOfBusiness.Models
{
    public class Sprzedawca
    {
        [Key]
        [Display(Name = "Numer id sprzedawcy")]
        public int id_sprzedawcy { get; set; }

        [Required]
        [Display(Name = "Login sprzedawcy")]
        public string login { get; set; }

        // Nawigacja do encji Konto i Uzytkownik, virtual dla lazy loading (chcemy lazy loading?)
        [Display(Name = "Konto sprzedawcy")]
        public virtual ICollection<Konto>? Konta { get; set; } = new List<Konto>();
        [Display(Name = "Użytkownik sprzedawcy aka osoba ktora ma tego sprzedawce (zazwyczaj ma tylko jednego)")]
        public virtual Uzytkownik? Uzytkownik { get; set; }
    }
}