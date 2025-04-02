using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DBContext
    {

        public class MBankContext : DbContext
        {

            public MBankContext()
            {

            }
            public MBankContext(DbContextOptions<MBankContext> options) : base(options)
            {

            }
            //public DbSet<Company> Companies { get; set; }
            //public DbSet<Invoice> Invoices { get; set; }


            protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@" Server=80.211.254.208; Database=ISZPDev;Trusted_Connection=false;user=Jacek.Patela;Password=1q2w3e4r;TrustServerCertificate=True;");


        }
    }
}
