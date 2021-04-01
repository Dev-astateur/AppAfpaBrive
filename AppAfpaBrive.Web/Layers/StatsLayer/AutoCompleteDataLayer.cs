using AppAfpaBrive.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppAfpaBrive.Web.Layers.StatsLayer
{
    public class AutoCompleteDataLayer
    {
        private readonly AFPANADbContext _dbContext = null;
        public AutoCompleteDataLayer(AFPANADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<EtablissementAutoComplete> GetAllEtablissementData()
        {
            return _dbContext.Etablissements.Select(x => new EtablissementAutoComplete
            {
                IdEtablissement = x.IdEtablissement,
                NomEtablissement = x.NomEtablissement
            }).ToList();
        }

        public List<OffreFormationAutoComplete> GetAllOffreFormationData()
        {
            return _dbContext.OffreFormations.Select(x => new OffreFormationAutoComplete
            {
                IdOffreFormation = x.IdOffreFormation,
                NomOffreFormation = x.LibelleOffreFormation
            }).ToList();
        }
    }

    public class EtablissementAutoComplete
    {
        public string IdEtablissement { get; set; }
        public string NomEtablissement { get; set; }
    }

    public class OffreFormationAutoComplete
    {
        public int IdOffreFormation { get; set; }
        public string NomOffreFormation { get; set; }
    }
}
