using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
{
    public class GetInsertionDataLayer
    {
        private readonly DAL.AFPANADbContext _dbContext = null;
        public GetInsertionDataLayer(DAL.AFPANADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<InsertionsTroisMois> GetInsertionTroisMois()
        {
            List<IInsertion> list = new List<IInsertion>();
            return _dbContext.InsertionTroisMois.Select(x => new InsertionsTroisMois
            {
                IdEtablissement = x.IdEtablissement,
                IdOffreFormation = x.IdOffreFormation,
                Annee = x.Annee,
                Cdi = x.Cdi,
                Cdd = x.Cdd,
                Alternance = x.Alternance,
                SansEmploie = x.SansEmploie,
                Autres = x.Autres
            }).ToList();
        }

        public List<InsertionsSixMois> GetInsertionSixMois()
        {
            List<IInsertion> list = new List<IInsertion>();
            return _dbContext.InsertionSixMois.Select(x => new InsertionsSixMois
            {
                IdEtablissement = x.IdEtablissement,
                IdOffreFormation = x.IdOffreFormation,
                Annee = x.Annee,
                Cdi = x.Cdi,
                Cdd = x.Cdd,
                Alternance = x.Alternance,
                SansEmploie = x.SansEmploie,
                Autres = x.Autres
            }).ToList();    
        }
        public List<InsertionsDouzeMois> GetInsertionDouzeMois()
        {
            List <IInsertion> list = new List<IInsertion>();
            return _dbContext.InsertionDouzeMois.Select(x => new InsertionsDouzeMois
            {
                IdEtablissement = x.IdEtablissement,
                IdOffreFormation = x.IdOffreFormation,
                Annee = x.Annee,
                Cdi = x.Cdi,
                Cdd = x.Cdd,
                Alternance = x.Alternance,
                SansEmploie = x.SansEmploie,
                Autres = x.Autres
            }).ToList();
        }

        public List<IInsertion> GetAllInsertion()
        {
            List<IInsertion> list = new List<IInsertion>();
            list.AddRange(GetInsertionTroisMois());
            list.AddRange(GetInsertionSixMois());
            list.AddRange(GetInsertionDouzeMois());
            return list;
        }

    }
}
