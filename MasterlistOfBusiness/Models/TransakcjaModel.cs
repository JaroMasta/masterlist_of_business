using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MasterlistOfBusiness.Models
{
    public class Transakcja
    {
        [Key]
        [Display(Name = "Numer id transakcji")]
        public int id_transakcji { get; set; }

        public int id_konta { get; set; }
        public int id_produktu { get; set; }

        [Display(Name = "Produkty")]
        public virtual ICollection<Produkt>? Produkty { get; set; } = new List<Produkt>();
    }
}