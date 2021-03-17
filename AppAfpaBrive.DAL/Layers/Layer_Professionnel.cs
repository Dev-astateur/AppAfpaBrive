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

        public List<List<string>> Get_Pro(int identreprise)
        {
            var x = _db.Professionnels.Join(_db.EntrepriseProfessionnels, x => x.IdProfessionnel, x => x.IdProfessionnel,
                (x, y) => new { x.NomProfessionnel, x.PrenomProfessionnel, y.IdProfessionnel, y.IdEntreprise })
                .Where(x => x.IdEntreprise == identreprise).ToArray();
            
            List<List<string>> listedeliste = new List<List<string>>();
            foreach (var item in x)
            {
                List<string> list = new List<string>();
                list.Add(item.IdProfessionnel.ToString());
                list.Add(item.PrenomProfessionnel);
                list.Add(item.NomProfessionnel);
                listedeliste.Add(list);
            }
            return listedeliste;
        }
    }

}
