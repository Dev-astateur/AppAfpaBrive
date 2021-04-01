using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;

namespace AppAfpaBrive.Web.Layers.StatsLayer
{
    public class InsertInsertionDataLayer
    {
        private readonly AFPANADbContext _dbContext = null;
        public InsertInsertionDataLayer(AFPANADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InsertAnwser(DestinataireEnquete anwser)
        {
            IInsertion InsertBase = GetLineToUpdate(anwser);

            /*A prendre en compte si on authorise les modification hors enquete
            //--------------------------------------------------------------------------
            if (IsAlreadyAnwsered(anwser.MatriculeBeneficiaire, anwser.IdPlanificationCampagneMailNavigation.Type))
            {
                InsertBase = GetUpdatedLineSoustraction(InsertBase, anwser.MatriculeBeneficiaire);
            }
            //--------------------------------------------------------------------------
            */

            InsertBase = GetUpdatedLineAddition(InsertBase, anwser.IdContratNavigation.TypeContrat);
            
            //switch (anwser.IdPlanificationCampagneMailNavigation.Type)
            //{
            //    case "1":
            //        _dbContext.InsertionTroisMois.Update((InsertionsTroisMois)InsertBase);
            //        break;

            //    case "2":
            //        _dbContext.InsertionSixMois.Update((InsertionsSixMois)InsertBase);
            //        break;

            //    case "3":
            //        _dbContext.InsertionDouzeMois.Update((InsertionsDouzeMois)InsertBase);
            //        break;

            //    default:
            //        throw new Exception("error");
            //}
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception dbException)
            {
                throw dbException;
            }
        }

        private IInsertion GetLineToUpdate(DestinataireEnquete anwser)
        {

            int annee = _dbContext.OffreFormations
                .Where(x => x.IdOffreFormation == anwser.IdOffreFormation && x.IdEtablissement == anwser.IdEtablissement)
                .Select(x => x.DateFinOffreFormation.Year).First();

            IInsertion toReturn = new NewLineStats();
           
            switch (anwser.IdPlanificationCampagneMailNavigation.Type)
            {
                case "1":
                    toReturn =  _dbContext.InsertionTroisMois
                    .Where(x => x.IdEtablissement == anwser.IdEtablissement &&
                    x.IdOffreFormation == anwser.IdOffreFormation &&
                    x.Annee == annee &&
                    x.EnLienAvecFormation == anwser.IdContratNavigation.EnLienMetierFormation).First();
                    break;
                case "2":
                    toReturn = _dbContext.InsertionSixMois
                    .Where(x => x.IdEtablissement == anwser.IdEtablissement &&
                    x.IdOffreFormation == anwser.IdOffreFormation &&
                    x.Annee == annee &&
                    x.EnLienAvecFormation == anwser.IdContratNavigation.EnLienMetierFormation).First();
                    break;
                case "3":
                    toReturn = _dbContext.InsertionDouzeMois
                    .Where(x => x.IdEtablissement == anwser.IdEtablissement &&
                    x.IdOffreFormation == anwser.IdOffreFormation &&
                    x.Annee == annee &&
                    x.EnLienAvecFormation == anwser.IdContratNavigation.EnLienMetierFormation).First();
                    break;
                default:
                    throw new Exception("Aucune ligne correspondante.");
            }
            int tmp = toReturn.TotalReponse + 1;
            toReturn.TotalReponse = tmp;
            return toReturn;
        }

        private IInsertion GetUpdatedLineAddition(IInsertion toUpdate, int typeContrat)
        {
            if (typeContrat == 1)
            {
                int tmp = toUpdate.Cdi + 1;
                toUpdate.Cdi = tmp;
            }
            else if (typeContrat == 2)
            {
                int tmp = toUpdate.Cdd + 1;
                toUpdate.Cdd = tmp;
            }
            else if (typeContrat == 3)
            {
                int tmp = toUpdate.Alternance + 1;
                toUpdate.Alternance = tmp;
            }
            else if (typeContrat == 4)
            {
                int tmp = toUpdate.SansEmploie + 1;
                toUpdate.SansEmploie = tmp;
            }
            else if (typeContrat == 5)
            {
                int tmp = toUpdate.Autres + 1;
                toUpdate.Autres = tmp;
            }
            else throw new Exception("Le type de contrat n'existe pas.");
            // ------------------------------
            return toUpdate;
        }

        // A utiliser en cas de changement hors enquete
        //private bool IsAlreadyAnwsered(string matriculeBeneficiaire, string type)
        //{
        //    bool isAnwsered = _dbContext.DestinataireEnquetes
        //        .Join(_dbContext.PlanificationCampagneMails,
        //        x => x.IdPlanificationCampagneMail,
        //        x => x.IdPlanificationCampagneMail,
        //        (t1, t2) => new
        //        {
        //            Type = t2.Type,
        //            Beneficiaire = t1.MatriculeBeneficiaire,
        //            Repondu = t1.Repondu
        //        }).Where(x => x.Type == type && x.Beneficiaire == matriculeBeneficiaire).Select(x => x.Repondu).First();
        //    return isAnwsered;
        //}

        // A utiliser en cas de changement hors enquete si quelqu'un a deja repondu et s'actuallise : if(IsAlreadyAnwsered() == true)
        //private IInsertion GetUpdatedLineSoustraction(IInsertion toUpdate, string matriculeBeneficiaire)
        //{
        //    int contrat = _dbContext.Contrats
        //        .Where(x => x.DestinataireEnquetes.Select(x => x.MatriculeBeneficiaire).First() == matriculeBeneficiaire)
        //        .Select(x => x.TypeContrat).First();
        //    int tmp;
        //    switch (contrat)
        //    {
        //        case 1:
        //            tmp = toUpdate.Cdi - 1;
        //            toUpdate.Cdi = tmp;
        //            break;
        //        case 2:
        //            tmp = toUpdate.Cdd - 1;
        //            toUpdate.Cdd = tmp;
        //            break;
        //        case 3:
        //            tmp = toUpdate.Alternance - 1;
        //            toUpdate.Alternance = tmp;
        //            break;
        //        case 4:
        //            tmp = toUpdate.SansEmploie - 1;
        //            toUpdate.SansEmploie = tmp;
        //            break;
        //        case 5:
        //            tmp = toUpdate.Autres - 1;
        //            toUpdate.Autres = tmp;
        //            break;
        //    }
        //    return toUpdate;
        //}

        // Comparaison entre la date de fin de formation et la date de real de l'enquete en mois dans le cas d'une maj hors enquete
        //private int SelectTablePourChangementPersonel(DestinataireEnquete anwser)
        //{
        //    DateTime dateReponse = (DateTime)anwser.DateRealisationEnquete;
        //    DateTime dateFinFormation = _dbContext.OffreFormations
        //        .Where(x => x.IdOffreFormation == anwser.IdOffreFormation && x.IdEtablissement == anwser.IdEtablissement)
        //        .Select(x => x.DateFinOffreFormation).First();

        //    long DiffInMonth = Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Month, dateReponse, dateFinFormation);

        //    if (DiffInMonth < 6) return 1;
        //    else if (DiffInMonth < 12) return 2;
        //    else return 3;
        //}
    }
}

