using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.DBContext;

namespace Repository
{
    public class RepositoryBase
    {
        public MBankContext db = new MBankContext(new DbContextOptionsBuilder<MBankContext>()
                .UseSqlServer(@" Server=80.211.254.208; Database=MBankRec_DeleteAfter_2025-04-30;Trusted_Connection=false;user=Jacek.Patela;Password=1q2w3e4r;TrustServerCertificate=True;")
                .Options);

    }
}
