
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MasterlistOfBusiness.Models;

namespace MasterlistOfBusiness.Data
{
    public class MasterlistOfBusinessContext : DbContext
    {
        public MasterlistOfBusinessContext (DbContextOptions<MasterlistOfBusiness> options)
            : base(options)
        {
        }

        public DbSet<MasterlistOfBusiness.Models.Sprzedawca> Sprzedawca { get; set; } = default!;
    }
}