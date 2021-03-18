using AppAfpaBrive.BOL;
using System;
using System.Linq;

namespace AppAfpaBrive.DAL.Layers.StatsLayer
{
    public class CreateNewLineDataLayer
    {
        private readonly AFPANADbContext _dbContext = null;
        public CreateNewLineDataLayer(AFPANADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private void CreateNewLine(IInsertion toInsert)
        {
            if (toInsert.IsValid())
            {
                _dbContext.InsertionTroisMois.Add((InsertionsTroisMois)toInsert);
                _dbContext.InsertionSixMois.Add((InsertionsSixMois)toInsert);
                _dbContext.InsertionDouzeMois.Add((InsertionsDouzeMois)toInsert);
            }
            else throw new Exception("Les champs IdEtablissement, IdOffreFormation et Annee doivent etre rempli.");
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
                throw new Exception("Type error");
            }
        }
    }
}
