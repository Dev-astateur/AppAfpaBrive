using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers.StatsLayer
{
    public class InsertInsertionDataLayer
    {
        private readonly AFPANADbContext _dbContext = null;
        public InsertInsertionDataLayer(AFPANADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InsertAnwser(IInsertion toInsert, string matriculeBeneficiaire, string type)
        {
            IInsertion InsertBase;
            switch (type)
            {
                case "1":
                    InsertBase = GetLineToUpdate((InsertionsTroisMois)toInsert);

                    if (IsAlreadyAnwsered(matriculeBeneficiaire, type))
                    { 
                        InsertBase = GetUpdatedLineSoustraction(InsertBase, matriculeBeneficiaire); 
                    }
                    InsertBase = GetUpdatedLineAddition(InsertBase, toInsert);

                    _dbContext.InsertionTroisMois.Update((InsertionsTroisMois)InsertBase);

                    break;

                case "2":
                    InsertBase = GetLineToUpdate((InsertionsSixMois)toInsert);

                    if (IsAlreadyAnwsered(matriculeBeneficiaire, type))
                    {
                        InsertBase = GetUpdatedLineSoustraction(InsertBase, matriculeBeneficiaire);
                    }
                    InsertBase = GetUpdatedLineAddition(InsertBase, toInsert);

                    _dbContext.InsertionSixMois.Update((InsertionsSixMois)InsertBase);

                    break;

                case "3":
                    InsertBase = GetLineToUpdate((InsertionsDouzeMois)toInsert);

                    if (IsAlreadyAnwsered(matriculeBeneficiaire, type))
                    {
                        InsertBase = GetUpdatedLineSoustraction(InsertBase, matriculeBeneficiaire);
                    }
                    InsertBase = GetUpdatedLineAddition(InsertBase, toInsert);

                    _dbContext.InsertionDouzeMois.Update((InsertionsDouzeMois)InsertBase);

                    break;

                default:
                    throw new Exception("error");
            }
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception dbException)
            {
                throw dbException;
            }
        }
        private bool IsAlreadyAnwsered(string matriculeBeneficiaire, string type)
        {
            bool isAnwsered = _dbContext.DestinataireEnquetes
                .Join(_dbContext.PlanificationCampagneMails,
                x => x.IdPlanificationCampagneMail,
                x => x.IdPlanificationCampagneMail,
                (t1, t2) => new
                {
                    Type = t2.Type,
                    Beneficiaire = t1.MatriculeBeneficiaire,
                    Repondu = t1.Repondu
                }).Where(x => x.Type == type && x.Beneficiaire == matriculeBeneficiaire).Select(x => x.Repondu).First();
            return isAnwsered;
        }
        private IInsertion GetLineToUpdate(IInsertion anwser)
        {
            if (anwser is InsertionsTroisMois)
            {
                return _dbContext.InsertionTroisMois.Where(x => x.IdEtablissement == anwser.IdEtablissement &&
                x.IdOffreFormation == anwser.IdOffreFormation && x.Annee == anwser.Annee).First();
            }
            else if (anwser is InsertionsSixMois)
            {
                return _dbContext.InsertionSixMois.Where(x => x.IdEtablissement == anwser.IdEtablissement &&
            x.IdOffreFormation == anwser.IdOffreFormation && x.Annee == anwser.Annee).First();
            }
            else if (anwser is InsertionsDouzeMois)
            {
                return _dbContext.InsertionDouzeMois.Where(x => x.IdEtablissement == anwser.IdEtablissement &&
               x.IdOffreFormation == anwser.IdOffreFormation && x.Annee == anwser.Annee).First();
            }
            else throw new Exception("Type error.");
        }
        private IInsertion GetUpdatedLineAddition(IInsertion toUpdate, IInsertion anwser)
        {
            // ------------------------------
            Type myType = anwser.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());//.Skip(3).ToList();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "IdOffreFormation" && prop.Name != "IdEtablissement" && prop.Name != "Annee")
                {
                    bool ok = Int32.TryParse((string)prop.GetValue(anwser, null), out int result);
                    if (ok && result == 1)
                    {
                        prop.SetValue(toUpdate, Int32.Parse((string)prop.GetValue(toUpdate, null) + 1));
                        break;
                    }
                }
            }
            // -------------------------------
            // La serie de if equivaut au foreach du dessus, il est plus clair et plus maintenable.
            // Garder celui du dessus si on ajoute des champs de contrat ou ajouter des if, 
            // prendre les if si besoin d'ajouter d'autres types de champs.
            // ------------------------------
            //if (anwser.Cdi == 1)
            //{
            //    int tmp = toUpdate.Cdi + 1;
            //    toUpdate.Cdi = tmp;
            //}
            //else if (anwser.Cdd == 1)
            //{
            //    int tmp = toUpdate.Cdd + 1;
            //    toUpdate.Cdd = tmp;
            //}
            //else if (anwser.Alternance == 1)
            //{
            //    int tmp = toUpdate.Alternance + 1;
            //    toUpdate.Alternance = tmp;
            //}
            //else if (anwser.SansEmploie == 1)
            //{
            //    int tmp = toUpdate.SansEmploie + 1;
            //    toUpdate.SansEmploie = tmp;
            //}
            //else if (anwser.Autres == 1)
            //{
            //    int tmp = toUpdate.Autres + 1;
            //    toUpdate.Autres = tmp;
            //}
            //else throw new Exception("Veuillez fournir un champs a 1.");
            // ------------------------------
            return toUpdate;
        }
        private IInsertion GetUpdatedLineSoustraction(IInsertion toUpdate, string matriculeBeneficiaire)
        {
            int contrat = _dbContext.Contrats
                .Where(x => x.DestinataireEnquetes.Select(x => x.MatriculeBeneficiaire).First() == matriculeBeneficiaire)
                .Select(x => x.TypeContrat).First();
            int tmp;
            switch (contrat)
            {
                case 1:
                    tmp = toUpdate.Cdi - 1;
                    toUpdate.Cdi = tmp;
                    break;
                case 2:
                    tmp = toUpdate.Cdd - 1;
                    toUpdate.Cdd = tmp;
                    break;
                case 3:
                    tmp = toUpdate.Alternance - 1;
                    toUpdate.Alternance = tmp;
                    break;
                case 4:
                    tmp = toUpdate.SansEmploie - 1;
                    toUpdate.SansEmploie = tmp;
                    break;
                case 5:
                    tmp = toUpdate.Autres - 1;
                    toUpdate.Autres = tmp;
                    break;
            }
            return toUpdate;
        }
    }
}
