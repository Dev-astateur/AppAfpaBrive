using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using System.Collections.Generic;
using System.Linq;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_Pays
    {
        private readonly AFPANADbContext _context;
        public Layer_Pays(AFPANADbContext context)
        {
            _context = context;
        }

        public IQueryable<string> Get_pays()
        {
            return _context.Pays.Select(x=>x.LibellePays);
        }

        public string Get_pays_ID(string Libelle)
        {
            return _context.Pays.Where(x=>x.LibellePays == Libelle).FirstOrDefault().Idpays2; 
        }
        public Pays GetPaysById(string idPays)
        {
            return _context.Pays.Find(idPays);
        }

        public IEnumerable<Pays> GetAll()
        {
            return _context.Pays.ToList();
        }

        public IEnumerable<string> GetAllLibelle()
        {
            return _context.Pays.Select(x => x.LibellePays).ToList();
        }

    }
}
