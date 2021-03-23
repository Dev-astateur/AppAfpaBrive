using System.Collections.Generic;
using AppAfpaBrive.BOL;
using System.Linq;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_Etablissement
    {
        private readonly AFPANADbContext _db;
        public Layer_Etablissement(AFPANADbContext context)
        {
            _db = context;
        }

        public IEnumerable<Etablissement> Get_Etablissement_Nom(string Id)
        {
            return _db.Etablissements.Where(x=>x.IdEtablissement == Id).ToList();
        }

        
    }
}
