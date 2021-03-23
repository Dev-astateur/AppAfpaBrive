using AppAfpaBrive.BOL;

namespace AppAfpaBrive.DAL.Layer
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
