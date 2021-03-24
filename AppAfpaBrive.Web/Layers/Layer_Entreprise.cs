using System.Collections.Generic;
using AppAfpaBrive.BOL;
using System.Linq;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_Entreprise
    {
        private readonly AFPANADbContext _db;
        public Layer_Entreprise(AFPANADbContext context)
        {
            _db = context;
        }

        public IEnumerable<Entreprise> get_Entreprise(string Siret)
        {
            return _db.Entreprises.Where(x=>x.NumeroSiret == Siret).ToList();
        }

        public void Create_entreprise(Entreprise entreprise)
        {
            _db.Entreprises.Add(entreprise);
            _db.SaveChanges();
        }

        public int Create_entreprise_ID_Back(Entreprise entreprise)
        {
            _db.Entreprises.Add(entreprise);
            _db.SaveChanges();
            return entreprise.IdEntreprise;
        }
    }
}
