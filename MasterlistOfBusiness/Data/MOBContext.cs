using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MasterlistOfBusiness.Models;

namespace MasterlistOfBusiness.Data
{
    public class MOBContext : DbContext
    {
        public MOBContext (DbContextOptions<MOBContext> options)
            : base(options)
        {
        }

        public DbSet<MasterlistOfBusiness.Models.Konto> Konto { get; set; } = default!;

        public DbSet<MasterlistOfBusiness.Models.Produkt> Produkt { get; set; } = default!;

        public DbSet<MasterlistOfBusiness.Models.Sprzedawca> Sprzedawca { get; set; } = default!;

        public DbSet<MasterlistOfBusiness.Models.Transakcja> Transakcja { get; set; } = default!;

        public DbSet<MasterlistOfBusiness.Models.Uzytkownik> Uzytkownik { get; set; } = default!;
    }
}
