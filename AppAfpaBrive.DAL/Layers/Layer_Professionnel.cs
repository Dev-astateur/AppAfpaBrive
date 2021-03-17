using System.Collections.Generic;
using System.Linq;

namespace AppAfpaBrive.DAL.Layer
{
    public class Layer_Professionnel
    {
        private readonly AFPANADbContext _db;
        public Layer_Professionnel(AFPANADbContext context)
        {
            _db = context;
        }

        public IQueryable<string> Get_Formation_Nom(int Id)
        {
            return _db.OffreFormations.Join(_db.ProduitFormations, x => x.CodeProduitFormation, x => x.CodeProduitFormation,
                (x, y) => new { x.CodeProduitFormation, x.IdOffreFormation, y.LibelleProduitFormation })
                .Where(x => x.IdOffreFormation == Id).Select(x => x.LibelleProduitFormation);
        }
    }

}
