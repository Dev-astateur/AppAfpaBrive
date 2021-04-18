﻿using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;
using AppAfpaBrive.Web.ModelView;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_ProduitDeFormation
    {
        private readonly AFPANADbContext _context;

        #region Constructeur
        public Layer_ProduitDeFormation(AFPANADbContext context)
        {
            _context = context;
        }
        
        #endregion
        #region Methode publique

        public List<ProduitFormation> GetProduitFormationStartWith(string codeProduit)
        {
            return _context.ProduitFormations.Where(x => x.CodeProduitFormation.ToString().StartsWith(codeProduit)).ToList();
        }

        public ProduitFormation GetByCodeProduitFormationdelete(int idCodeProduitFormation)
        {
            return _context.ProduitFormations.Find(idCodeProduitFormation);
        }
        public ProduitFormationModelView GetByCodeProduitFormation(int idCodeProduitFormation)
        {
            var obj =_context.ProduitFormations.Select(e => new ProduitFormationModelView()
            {
                CodeProduitFormation = e.CodeProduitFormation,
                NiveauFormation = e.NiveauFormation,
                LibelleProduitFormation = e.LibelleProduitFormation,
                LibelleCourtFormation = e.LibelleCourtFormation,
                //FormationContinue = e.FormationContinue,
                //FormationDiplomante = e.FormationDiplomante
            }).First(p => p.CodeProduitFormation == idCodeProduitFormation);
            return obj as ProduitFormationModelView;
        }




        public async Task<PagingList<ProduitFormation>> GetPage(string filter,int page = 1,string sortExpression ="CodeProduitFormation")
        {
            var qry = _context.ProduitFormations.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.LibelleProduitFormation.Contains(filter));
            }
            
            return await PagingList.CreateAsync<ProduitFormation>(qry,20, page, sortExpression,"CodeProduitFormation");
        }

        public void InsertProduit(ProduitFormation prod)
        {
            var check=CheckCodeProduitExiste(prod.CodeProduitFormation);
            if (check == true)
            {
                _context.ProduitFormations.Add(prod);
                _context.SaveChanges();
            }
            
        }
        public void Remove(ProduitFormation prod)
        {
            _context.ProduitFormations.Remove(prod);
            _context.SaveChanges();
        }
        public void Update(ProduitFormation prod)
        {
            _context.ProduitFormations.Update(prod);
            _context.SaveChanges();
        }
        
        public bool CheckCodeProduitExiste(int id)
        {
            var codeproduit = _context.ProduitFormations.Find(id);

            if (codeproduit == null)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}