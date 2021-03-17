using System.Linq;

namespace AppAfpaBrive.DAL.Layer
{
    public class Layer_Pays
    {
        private readonly AFPANADbContext _db;
        public Layer_Pays(AFPANADbContext context)
        {
            _db = context;
        }

        public IQueryable<string> Get_pays()
        {
            return _db.Pays.Select(x=>x.LibellePays);
        }

        public string Get_pays_ID(string Libelle)
        {
            return _db.Pays.Find(Libelle).Idpays2;
        }
    }
}
