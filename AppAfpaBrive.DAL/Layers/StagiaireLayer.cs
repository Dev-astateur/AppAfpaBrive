using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.DAL.Layers
{
    
    public class StagiaireLayer
    {
        private readonly AFPANADbContext _context;
        public StagiaireLayer(AFPANADbContext context)
        {
            _context = context;
        }

        //Methode pour aller chercher tous les stagiaires
        public List<Beneficiaire> GetAllStagiaires()
        {
            return _context.Beneficiaires.ToList();
        }
        public Beneficiaire FinByMatricule(string matricule)
        {
            return _context.Beneficiaires.Find(new object[] { matricule });
        }
        //Methode pour aller chercher les stagiaires par offre de formation
        //public List<Beneficiaire> GetBeneficaireByOffreFormation(string query)
        //{
        //    return _context.Beneficiaires.
        //}

    }
}
