using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MasterlistOfBusiness.Models
{
    public class Konto
    {
        [Key]
        [Display(Name = "Numer id konta")]
        public int id_konta { get; set; }
        public int? id_sprzedawcy { get; set; }

        [Display(Name = "Link do konta")]
        [StringLength(500, ErrorMessage = "Link nie może przekraczać 500 znaków.")]
        [DisplayFormat(NullDisplayText = "Brak")]
        public string? link { get; set; }

        [Required]
        [Display(Name = "Nazwa użytkownika")]
        [StringLength(100, ErrorMessage = "Nazwa użytkownika nie może przekraczać 100 znaków.")]
        public string NazwaUzytkownika { get; set; }

        [Display(Name = "Nazwa platformy")]
        [StringLength(100, ErrorMessage = "Nazwa platformy nie może przekraczać 100 znaków.")]
        public string? Platforma { get; set; }

        // Nawigacja do encji Sprzedawca, virtual dla lazy loading (chcemy lazy loading?)
        [Display(Name = "Sprzedawca")]
        public virtual Sprzedawca Sprzedawca { get; set; }
        
        public virtual ICollection<Inwentarz>? Inwentarze { get; set; } = new List<Inwentarz>();
    }

}