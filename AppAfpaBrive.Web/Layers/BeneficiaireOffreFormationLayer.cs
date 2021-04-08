using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OpenXmlHelpers.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.Web.Layers
{
    public class BeneficiaireOffreFormationLayer
    {
        private readonly AFPANADbContext _context;
        
        #region Constructeur
        public BeneficiaireOffreFormationLayer(AFPANADbContext context)
        {
            this._context = context;
            
        }
        #endregion
        #region Methode publique
        public ICollection <BOL.BeneficiaireOffreFormation> GetAllByOffreFormation(int id)
        {
            return _context.BeneficiaireOffreFormations.Where(e => e.IdOffreFormation == id).ToList();
        }
        public ICollection<BOL.BeneficiaireOffreFormation> GetAll()
        {
            return _context.BeneficiaireOffreFormations.ToList();
        }
        #endregion
       
    }

}
