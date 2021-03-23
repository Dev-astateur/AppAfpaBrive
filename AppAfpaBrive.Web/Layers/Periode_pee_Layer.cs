using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers
{
    public class Periode_pee_Layer
    {
        private readonly AFPANADbContext _dbContext = null;

        public Periode_pee_Layer(AFPANADbContext context)
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
