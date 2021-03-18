﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppAfpaBrive.DAL;
using AppAfpaBrive.BOL;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;

namespace AppAfpaBrive.DAL.Layers
{
    public class ProduitDeFormationLayer
    {
        private readonly AFPANADbContext _context;

        public ProduitDeFormationLayer(AFPANADbContext context)
        {
            _context = context;
        }
       
        public BOL.ProduitFormation GetByCodeProduitFormation(int idCodeProduitFormation)
        {
            return _context.ProduitFormations.Find(idCodeProduitFormation);
        }
        public async Task<PagingList<ProduitFormation>> GetPage(int page = 1,string sortExpression ="CodeProduitFormation")
        {
            var qry= _context.ProduitFormations;
            return await PagingList.CreateAsync<ProduitFormation>(qry,20, page, sortExpression,"CodeProduitFormation");
        }

        public void InsertProduit(ProduitFormation prod)
        {
            _context.ProduitFormations.Add(prod);
            _context.SaveChanges();
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
        
    }
}
