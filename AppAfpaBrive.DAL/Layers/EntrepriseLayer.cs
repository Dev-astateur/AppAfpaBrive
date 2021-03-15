using AppAfpaBrive.BOL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.DAL.Layers
{
   public class EntrepriseLayer
    {
        private readonly AFPANADbContext _context;

        public EntrepriseLayer(AFPANADbContext context)
        {
            _context = context;
        }

       public List<Entreprise> GetAllEntreprise()
        {
            return _context.Entreprises.ToList();
        }
        public List<Entreprise> GetEntreprisesByDepartement(string dep)
        {
            return _context.Entreprises.Where(e => e.CodePostal.StartsWith(dep)).ToList();
        }
        public List<Entreprise> GetEntrepriseByProduitFormation(string offre)
        {
            //var query = _context.Entreprises.Join(_context.Contrats
            //                                , e => e.IdEntreprise
            //                                , c => c.IdEntreprise
            //                                , (e, c) => new
            //                                {
            //                                    contratMatriculeBen = c.MatriculeBeneficiaire
            //                                })
            //                                .Join(_context.Beneficiaires
            //                                , c => c.contratMatriculeBen
            //                                , b => b.MatriculeBeneficiaire
            //                                , (contrat, benef) => new
            //                                {
            //                                    matriculeBenf = benef.MatriculeBeneficiaire
            //                                })
            //                                .Join(_context.Pees,
            //                                benef => benef.matriculeBenf
            //                                , pees => pees.MatriculeBeneficiaire,
            //                                (bene, pee) => new
            //                                {
            //                                    idOffre = pee.IdOffreFormation
            //                                })
            //                                .Join(_context.OffreFormations
            //                                , pee => pee.idOffre
            //                                , offreForm => offreForm.IdOffreFormation
            //                                , (pee, OffreForm) => new
            //                                {

            //                                    codeproduitFormation = OffreForm.CodeProduitFormation
            //                                })
            //                                .Join(_context.ProduitFormations,
            //                                offre => offre.codeproduitFormation
            //                                , prod => prod.CodeProduitFormation
            //                                , (offre, prod) => new
            //                                {
            //                                    intitule = prod.LibelleProduitFormation
            //                                })
            //                                .Where(pro => pro.intitule.Contains(offre)).ToList();


            List<Entreprise> query2 = _context.ProduitFormations
                                           .Where(pro => pro.LibelleProduitFormation.Contains(offre))
                                            .Join(_context.OffreFormations
                                           , p => p.CodeProduitFormation
                                           , o => o.CodeProduitFormation
                                           , (p, o) => new
                                           {
                                               idoffre = o.IdOffreFormation
                                           })
                                           .Join(_context.Pees
                                           , o => o.idoffre
                                           , p => p.IdOffreFormation
                                           , (o, pee) => new
                                           {
                                               matriculeBenef = pee.MatriculeBeneficiaire
                                           })
                                           .Join(_context.Beneficiaires,
                                           p => p.matriculeBenef
                                           , bene => bene.MatriculeBeneficiaire,
                                           (bene, pee) => new
                                           {
                                               matriculeBenef = pee.MatriculeBeneficiaire
                                           })
                                           .Join(_context.Contrats
                                           , b => b.matriculeBenef
                                           , c => c.MatriculeBeneficiaire
                                           , (b, c) => new
                                           {

                                               idEntrepretrise = c.IdEntreprise
                                           })
                                           .Join(_context.Entreprises,
                                           c => c.idEntrepretrise
                                           , ent => ent.IdEntreprise
                                           , (c, e) => new Entreprise
                                           {
                                               IdEntreprise=e.IdEntreprise,
                                               RaisonSociale=e.RaisonSociale,
                                               NumeroSiret=e.NumeroSiret,
                                               MailEntreprise=e.MailEntreprise,
                                               TelEntreprise=e.TelEntreprise,
                                               Ligne1Adresse=e.Ligne1Adresse,
                                               CodePostal=e.CodePostal,
                                               Ville=e.Ville,
                                               Idpays2=e.Idpays2                      
                                           }).ToList();

            return query2;








        }


    }
}
