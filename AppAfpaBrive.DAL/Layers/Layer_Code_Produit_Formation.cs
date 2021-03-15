using System.Collections.Generic;
using AppAfpaBrive.BOL;
using System.Linq;

namespace AppAfpaBrive.DAL.Layer
{
    public class Layer_Code_Produit_Formation
    {
        private readonly AFPANADbContext _db;
        public Layer_Code_Produit_Formation(AFPANADbContext context)
        {
            _db = context;
        }

        public IEnumerable<ProduitFormation> Get_Etablissement_Nom(int Id)
        {
            return _db.ProduitFormations.Where(x=>x.CodeProduitFormation == Id);
        }
    }
}
