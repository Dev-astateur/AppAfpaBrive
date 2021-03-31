﻿using System.Collections.Generic;
using AppAfpaBrive.BOL;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace AppAfpaBrive.DAL.Layer
{
    public class Layer_Code_Produit_Formation
    {
        private readonly AFPANADbContext _db;
        public Layer_Code_Produit_Formation(AFPANADbContext context)
        {
            _db = context;
        }

        public IQueryable<string> Get_Formation_Nom(int Id)
        {
            return _db.OffreFormations.Join(_db.ProduitFormations, x => x.CodeProduitFormation, x => x.CodeProduitFormation,
                (x, y) => new { x.CodeProduitFormation, x.IdOffreFormation, y.LibelleProduitFormation })
                .Where(x => x.IdOffreFormation == Id).Select(x => x.LibelleProduitFormation);
        }

        public List<IEnumerable<string>> GetCodeRomeProduitFormationByDestinataireEnquete(DestinataireEnquete de)
        {
            var of = _db.OffreFormations.Where(of => of.IdEtablissement == de.IdEtablissement && of.IdOffreFormation == de.IdOffreFormation);

            var pf = of.Select(o => o.CodeProduitFormationNavigation);

            var ro = pf.Select(p => p.ProduitFormationAppellationRomes.Select(r => r.CodeRome)).ToList();
            return ro ; 
        }
    }

}
