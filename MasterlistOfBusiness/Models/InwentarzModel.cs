using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MasterlistOfBusiness.Models
{
public class Inwentarz
    {   
        [Key]
        public int id_inwentarza { get; set; }
        public int id_konta { get; set; }

        public int id_produktu { get; set; }

        [Display(Name = "Cena")]
        [Range(1, int.MaxValue, ErrorMessage = "Cena musi być wartoscia calkowita wieksza niz 0.")]
        public int cena { get; set; }

        [Display(Name = "Ilosc")]
        [Range(0, int.MaxValue, ErrorMessage = "Ilosc musi byc wartoscia calkowita wieksza lub rowna 0.")]
        public int ilosc { get; set; } = 0;

        // Nawigacje do encji konto i produkt, virtual dla lazy loading (chcemy lazy loading?)
        [Display(Name = "Konto")]
        public virtual Konto Konto { get; set; }

        [Display(Name = "Produkt")]
        public virtual Produkt Produkt { get; set; }
    }

}