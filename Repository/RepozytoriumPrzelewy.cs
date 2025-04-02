using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepozytoriumPrzelewy : RepositoryBase
    {
        public List<Przelew> GetAll()
        {
            return db.Przelewy.ToList();
        }

        public void DodajPrzelew(Przelew przelew)
        {
            db.Przelewy.Add(przelew);
            db.SaveChanges();
        }

        public void DodajPrzelewProcedura(Przelew przelew)
        {
            ValidujIRegenerujGuid(przelew);
            db.Database.ExecuteSqlRaw(
            "EXEC DodajPrzelew @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8",
            przelew.Id,
            przelew.LP,
            przelew.Kwota,
            przelew.RachunekNadawcy,
            przelew.RachunekOdbiorcy,
            przelew.Opis,
            przelew.DodanyDnia,
            (int)przelew.TrybDodania,
            przelew.PlikPochodzenia);

            db.SaveChanges();
        }


        private void ValidujIRegenerujGuid(Przelew przelew)
        {
            if (przelew.Id == Guid.Empty)
            {
                przelew.Id = Guid.NewGuid();
            }
        }
    }
}