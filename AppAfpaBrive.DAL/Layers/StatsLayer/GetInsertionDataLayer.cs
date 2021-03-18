using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.DAL.Layers.StatsLayer
{
    public class GetInsertionDataLayer
    {
        private readonly DAL.AFPANADbContext _dbContext = null;
        public GetInsertionDataLayer(DAL.AFPANADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<IInsertion> GetInsertionTroisMois()
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
            }).ToList<IInsertion>();
        }
        public List<IInsertion> GetInsertionSixMois()
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
            }).ToList<IInsertion>();    
        }
        public List<IInsertion> GetInsertionDouzeMois()
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
            }).ToList<IInsertion>();
        }
        public List<IInsertion> GetAllInsertion()
        {
            List<IInsertion> list = new List<IInsertion>();
            list.AddRange(GetInsertionTroisMois());
            list.AddRange(GetInsertionSixMois());
            list.AddRange(GetInsertionDouzeMois());
            return list;
        }
        public List<IInsertion> GetOneYearData(List<IInsertion> datas, int annee)
        {
            return datas.Where(x => x.Annee == annee).ToList();
        }
        public List<IInsertion> GetOneFormationData(List<IInsertion> datas, int idOffreFormation)
        {
            return datas.Where(x => x.IdOffreFormation == idOffreFormation).ToList();
        }
        public List<IInsertion> GetOneEtablissementData(List<IInsertion> datas, string idEtablissement)
        {
            return datas.Where(x => x.IdEtablissement == idEtablissement).ToList();
        }
    }
}
