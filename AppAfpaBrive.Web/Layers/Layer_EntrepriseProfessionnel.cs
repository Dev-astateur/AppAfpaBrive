using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_EntrepriseProfessionnel
    {
        private readonly AFPANADbContext _db;
        public Layer_EntrepriseProfessionnel(AFPANADbContext context)
        {
            _db = context;
        }
        public void create(EntrepriseProfessionnel entreprise)
        {
            _db.Add(entreprise);
            _db.SaveChanges();
        }
        
    }

}
