using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_Periode_pee
    {
        private readonly AFPANADbContext _dbContext = null;

        public Layer_Periode_pee(AFPANADbContext context)
        {
            _dbContext = context;
        }
        public void Pee_Create(PeriodePee pee)
        {
            _dbContext.PeriodePees.Add(pee);
            _dbContext.SaveChanges();
        }
    }
}
