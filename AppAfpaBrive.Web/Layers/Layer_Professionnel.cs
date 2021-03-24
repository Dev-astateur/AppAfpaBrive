using System.Collections.Generic;
using System.Linq;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers
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

        public void create(Professionnel pro)
        {
            _db.Add(pro);
            _db.SaveChanges();
        }

        public int Get_Id_pro(string nom , string prenom)
        {
            return _db.Professionnels.Where(x => x.NomProfessionnel == nom && x.PrenomProfessionnel == prenom).FirstOrDefault().IdProfessionnel;
        }

        public Professionnel GetProfessionnel(int ID)
        {
            return _db.Professionnels.Where(x => x.IdProfessionnel == ID).FirstOrDefault();
        }

        public string Get_Nom_Pro(int ID)
        {
            return _db.Professionnels.Where(x => x.IdProfessionnel == ID).Select(x => x.NomProfessionnel).FirstOrDefault();
        }
        public string Get_Prenom_Pro(int ID)
        {
            return _db.Professionnels.Where(x => x.IdProfessionnel == ID).Select(x => x.PrenomProfessionnel).FirstOrDefault();
        }

    }

}
