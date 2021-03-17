using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppAfpaBrive.DAL;
using AppAfpaBrive.BOL;


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
        public IEnumerable<BOL.ProduitFormation> GetAll()
        {
            return _context.ProduitFormations.OrderBy(x => x.CodeProduitFormation);
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
