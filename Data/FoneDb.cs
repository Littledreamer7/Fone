using FoneApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FoneApi.Data
{
    public partial class FoneDb :DbContext
    {
        public FoneDb(DbContextOptions<FoneDb> options) : base(options)
        {

        }
        public DbSet<Fone_Model> FoneApi_Tbl { get; set; }
        public DbSet<Fone_Customer> Fone_Customers { get; set; }
       
    }
}
