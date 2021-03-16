﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.DAL.Layers
{
    public class OffreDeFormationLayer
    {
        private readonly AFPANADbContext _context;

        #region Constructeur
        public OffreDeFormationLayer(AFPANADbContext context)
        {
            this._context = context;
        }
        #endregion
        #region Methode publique
        public BOL.OffreFormation GetByMatriculeCollaborateurAFPA(string idCollaborateurAFPA)
        {
            return _context.OffreFormations.Where(a => a.MatriculeCollaborateurAfpa == idCollaborateurAFPA)
                .Include(e => e.BeneficiaireOffreFormations).ThenInclude(a => a.MatriculeBeneficiaireNavigation)
               .FirstOrDefault();
           // return _context.OffreFormations.Where(b => b.MatriculeCollaborateurAfpa == idCollaborateurAFPA).FirstOrDefault();
                
        }
        public ICollection<BOL.OffreFormation> GetAllbyMatricule(string idCollaborateurAFPA)
        {
            return _context.OffreFormations.Where(a => a.MatriculeCollaborateurAfpa == idCollaborateurAFPA).ToList();
               
        }
        
        #endregion
    }
}
