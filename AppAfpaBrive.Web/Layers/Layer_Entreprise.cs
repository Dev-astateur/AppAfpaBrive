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

        public IEnumerable<Entreprise> get_Entreprise(string Siret)
        {
            return _db.Entreprises.Where(x=>x.NumeroSiret == Siret).ToList();
        }

        public void Create_entreprise(Entreprise entreprise)
        {
            _db.Entreprises.Add(entreprise);
            _db.SaveChanges();
        }

        public Entreprise GetEntrepriseById(int id)
        {
            Entreprise entreprise = _db.Entreprises.Where(e => e.IdEntreprise == id).FirstOrDefault();
            return entreprise; 
        }
    }
}
