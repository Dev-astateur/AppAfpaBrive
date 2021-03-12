using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AppAfpaBrive.BOL;
using System.Linq;

namespace AppAfpaBrive.DAL.Layer
{
    class Layer_Offre_Formation
    {
        private readonly AFPANADbContext _db;
        public Layer_Offre_Formation(AFPANADbContext context)
        {
            _db = context;
        }
        
        public IEnumerable<BeneficiaireOffreFormation> GetFormations (string matricule)
        {
            return _db.BeneficiaireOffreFormations.Where(x => x.MatriculeBeneficiaire == matricule).ToList();
        }
    }
}
