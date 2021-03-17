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

        public IQueryable<string> Get_Pro(int identreprise)
        {
            var x =_db.EntrepriseProfessionnels.Join(_db.Professionnels, x => x.IdProfessionnel, x => x.IdProfessionnel,
                (x, y) => new { x.IdEntreprise, x.IdProfessionnel, y.NomProfessionnel, y.PrenomProfessionnel })
                .Where(x => x.IdEntreprise == identreprise).ToList();
            return (IQueryable<string>)x;
        }
    }

}
