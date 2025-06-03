using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MasterlistOfBusiness.Models
{
    public class Uzytkownik
    {
        [Key]
        [Display(Name = "Login")]
        public string login { get; set; }

        public string? HasloHash { get; set; }

        [Display(Name = "Typ konta")]
        //TODO: Zamienić na enum
        public string? typ { get; set; }


        [Required]
        [NotMapped]
        [Display(Name = "Hasło")]
        public string Haslo { get; set; }
        
        // Nawigacja do encji Sprzedawca
        public virtual ICollection<Sprzedawca>? Sprzedawca { get; set; } = new List<Sprzedawca>();
    }
}
