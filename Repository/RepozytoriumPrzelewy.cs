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
            //throw new NotImplementedException();
            return db.Przelewy.ToList();
        }

        ///// <summary>
        ///// Retrieves the latest invoice number for a particual issuer.
        ///// </summary>
        ///// <param name="issuerId"></param>
        ///// <returns></returns>
        //public (string?, DateTime) GetLatestInvoiceNumber(Guid issuerId)
        //{
        //    var latestInvoice = db.Przelewy
        //        .Where(w => w.Issuer.Id == issuerId)
        //        .OrderBy(o => o.CreatedAt)
        //        .FirstOrDefault();
        //    if (latestInvoice is not null)
        //    {
        //        return (latestInvoice.Number, latestInvoice.IssueDate);
        //    }
        //    return (null, DateTime.Today);
        //}
    }
}
