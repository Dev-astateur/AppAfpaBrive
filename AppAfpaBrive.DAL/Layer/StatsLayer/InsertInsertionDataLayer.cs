using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AppAfpaBrive.DAL.Layer.StatsLayer
{
    public class InsertInsertionDataLayer
    {
        private readonly AFPANADbContext _dbContext = null;
        public InsertInsertionDataLayer(AFPANADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateNewLine(IInsertion toInsert)
        {
            if (toInsert.IsValid())
            {
                _dbContext.InsertionTroisMois.Add((InsertionsTroisMois)toInsert);
                _dbContext.InsertionSixMois.Add((InsertionsSixMois)toInsert);
                _dbContext.InsertionDouzeMois.Add((InsertionsDouzeMois)toInsert);

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception dbException)
                {
                    throw dbException;
                }
            }
            else throw new Exception("Les champs IdEtablissement, IdOffreFormation et Annee doivent etre rempli.");       
        }

        public void AddOneInsertion(IInsertion anwser)
        {
            if (anwser is InsertionsTroisMois obj3) AddOne(obj3);
            else if (anwser is InsertionsSixMois obj6) AddOne(obj6);
            else if (anwser is InsertionsDouzeMois obj12) AddOne(obj12);
            else throw new Exception("Erreur inconnu.");
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception dbException)
            {
                throw dbException;
            }
        }

        #region Get updatedLine (Addition ou soustraction)
        private static IInsertion GetUpdatedLineAddition(IInsertion toUpdate, IInsertion anwser)
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

        private static IInsertion GetUpdatedLineSoustraction(IInsertion toUpdate, IInsertion anwser)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Get the line to update 
        public IInsertion GetLineToUpdate(InsertionsTroisMois anwser)
        {
            return _dbContext.InsertionTroisMois.Where(x => x.IdEtablissement == anwser.IdEtablissement && 
            x.IdOffreFormation == anwser.IdOffreFormation && x.Annee == anwser.Annee).First();
        }
        public IInsertion GetLineToUpdate(InsertionsSixMois anwser)
        {
            return _dbContext.InsertionSixMois.Where(x => x.IdEtablissement == anwser.IdEtablissement &&
            x.IdOffreFormation == anwser.IdOffreFormation && x.Annee == anwser.Annee).First();
        }
        public IInsertion GetLineToUpdate(InsertionsDouzeMois anwser)
        {
            return _dbContext.InsertionDouzeMois.Where(x => x.IdEtablissement == anwser.IdEtablissement &&
            x.IdOffreFormation == anwser.IdOffreFormation && x.Annee == anwser.Annee).First();
        }
        #endregion

        #region Add one to an existing row
        private void AddOne(InsertionsTroisMois obj)
        {
            var toUpdate = GetLineToUpdate(obj);
            _dbContext.InsertionTroisMois.Update((InsertionsTroisMois)GetUpdatedLineAddition(toUpdate, obj));    
        }
        private void AddOne(InsertionsSixMois obj)
        {
            var toUpdate = GetLineToUpdate(obj);
            _dbContext.InsertionSixMois.Update((InsertionsSixMois)GetUpdatedLineAddition(toUpdate, obj));   
        }
        private void AddOne(InsertionsDouzeMois obj)
        {
            var toUpdate = GetLineToUpdate(obj);
            _dbContext.InsertionDouzeMois.Update((InsertionsDouzeMois)GetUpdatedLineAddition(toUpdate, obj));           
        }
        #endregion
    }
}
