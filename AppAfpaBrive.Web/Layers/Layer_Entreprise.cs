using System.Collections.Generic;
using AppAfpaBrive.BOL;
using System.Linq;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layer
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

        public Entreprise GetEntrepriseById(int id)
        {
            Entreprise entreprise = _db.Entreprises.Where(e => e.IdEntreprise == id).FirstOrDefault();
            return entreprise; 
        }

        public List<string> GetAllSirets()
        {
            return _db.Entreprises.Select(entr=>entr.NumeroSiret).ToList(); 
        }
    }
}
