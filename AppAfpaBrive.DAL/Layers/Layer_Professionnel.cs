using System.Collections.Generic;
using System.Linq;
using AppAfpaBrive.BOL;

namespace AppAfpaBrive.DAL.Layer
{
    public class Layer_Professionnel
    {
        private readonly AFPANADbContext _db;
        public Layer_Professionnel(AFPANADbContext context)
        {
            _db = context;
        }

        public List<Professionnel> Get_Pro(int identreprise)
        {
            var x = _db.Professionnels.Join(_db.EntrepriseProfessionnels, x => x.IdProfessionnel, x => x.IdProfessionnel,
                (x, y) => new { x.NomProfessionnel, x.PrenomProfessionnel, y.IdProfessionnel, y.IdEntreprise })
                .Where(x => x.IdEntreprise == identreprise)
                .Select(x=> new { x.NomProfessionnel,x.PrenomProfessionnel,x.IdProfessionnel}).ToList();
            List<Professionnel> y = new List<Professionnel>();
            foreach (var item in x)
            {
                Professionnel list = new Professionnel { 
                IdProfessionnel= item.IdProfessionnel,
                NomProfessionnel=item.NomProfessionnel,
                PrenomProfessionnel=item.PrenomProfessionnel
                };
                y.Add(list);
            }
            return y;
        }
    }

}
