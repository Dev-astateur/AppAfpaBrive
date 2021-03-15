using System.Collections.Generic;
using AppAfpaBrive.BOL;
using System.Linq;

namespace AppAfpaBrive.DAL.Layer
{
    public class Layer_Entreprise
    {
        private readonly AFPANADbContext _db;
        public Layer_Entreprise(AFPANADbContext context)
        {
            _db = context;
        }

        public IEnumerable<Entreprise> get_Formation(string Siret)
        {
            return _db.Entreprises.Where(x=>x.NumeroSiret == Siret).ToList();
        }
    }
}
