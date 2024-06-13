using Microsoft.EntityFrameworkCore;
using VillaAPI.Models;

namespace VillaAPI.Data
{
    public class ApplicationDbContext: DbContext

    {
        public DbSet<Villa> Villas { get; set; } //Villas name of the table in the database
    }
}
