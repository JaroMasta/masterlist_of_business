using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MasterlistOfBusiness.Models
{
    public class Produkt
    {
        [Key]
        [Display(Name = "Numer id produktu")]
        public int id_produktu { get; set; }

        [Required]
        [Display(Name = "Typ produktu")]
        [StringLength(100, ErrorMessage = "Typ produktu nie może przekraczać 100 znaków.")]
        public string typ { get; set; }

        [Display(Name = "Opis produktu")]
        [StringLength(500, ErrorMessage = "Opis produktu nie może przekraczać 500 znaków.")]
        public string? opis { get; set; }

        // Nawigacja do encji Inwentarz, virtual dla lazy loading (chcemy lazy loading?)
        public virtual ICollection<Inwentarz>? Inwentarze { get; set; } = new List<Inwentarz>();
    }
}