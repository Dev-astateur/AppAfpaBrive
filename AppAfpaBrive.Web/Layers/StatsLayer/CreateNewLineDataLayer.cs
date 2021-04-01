using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AppAfpaBrive.Web.Layers.StatsLayer
{
    public class CreateNewLineDataLayer
    {
        private readonly AFPANADbContext _dbContext = null;
        public CreateNewLineDataLayer(AFPANADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateNewLine(NewLineStats toInsert)
        {
            
            if (toInsert.IsValid())
            {
                NewLineFormation(toInsert, true);
                NewLineFormation(toInsert, false);
            }
            else throw new Exception("Les champs IdEtablissement, IdOffreFormation, Annee et EnLienAvecFormation doivent etre rempli.");

            try
            {
                _dbContext.SaveChanges();
            }
            catch(Exception dbException)
            {
                throw new Exception(dbException.Message);
            }
        }

        private void NewLineFormation(NewLineStats toInsert, bool lien)
        {
            toInsert.EnLienAvecFormation = lien;
            try
            {
                _dbContext.InsertionTroisMois.Add(toInsert.ToInsertionTroisMois());
                _dbContext.InsertionSixMois.Add(toInsert.ToInsertionSixMois());
                _dbContext.InsertionDouzeMois.Add(toInsert.ToInsertionDouzeMois());
            }
            catch(Exception ex)
            {
                throw new Exception("Doublons");
            }
            }




        private bool IsLineExisting(IInsertion anwser)
        {
            if (anwser is InsertionsTroisMois)
            {
                return _dbContext.InsertionTroisMois.Where(x => x.IdEtablissement == anwser.IdEtablissement &&
                x.IdOffreFormation == anwser.IdOffreFormation && x.Annee == anwser.Annee).Count() == 1;
            }
            else if (anwser is InsertionsSixMois)
            {
                return _dbContext.InsertionSixMois.Where(x => x.IdEtablissement == anwser.IdEtablissement &&
                x.IdOffreFormation == anwser.IdOffreFormation && x.Annee == anwser.Annee).Count() == 1;
            }
            else if (anwser is InsertionsDouzeMois)
            {
                return _dbContext.InsertionDouzeMois.Where(x => x.IdEtablissement == anwser.IdEtablissement &&
                x.IdOffreFormation == anwser.IdOffreFormation && x.Annee == anwser.Annee).Count() == 1;
            }
            else
            {
                throw new Exception("Type error.");
            }
        }
    }
    
}
