using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
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

        public PagingList<Entreprise> GetAllEntrepriseForPaging(int page = 1)
        {
            var query = _context.Entreprises.OrderBy(e => e.RaisonSociale);
            return PagingList.Create<Entreprise>(query, 10, page);
        }

        //Methode asynchrone 

        //public async Task<PagingList<Entreprise>> GetAllEntrepriseForPaging(int page)
        //{
        //   var query= _context.Entreprises.OrderBy(e=>e.RaisonSociale);
        //    return await PagingList.CreateAsync<Entreprise>(query, 10, page);
        //}


        public List<Entreprise> GetEntreprisesByDepartement(string dep)
        {
            return _context.Entreprises.Where(e => e.CodePostal.StartsWith(dep)).ToList();
        }

        public PagingList<Entreprise> GetEntreprisesByDepartementPaging(string dep, int page = 1)
        {
            var query = _context.Entreprises.Where(e => e.CodePostal.StartsWith(dep)).OrderBy(e => e.RaisonSociale);

            return PagingList.Create<Entreprise>(query, 10, page);
        }
        public void RemoveEntrepriseById(int id)
        {
            _context.Entreprises.Remove(GetEntrepriseById(id));
            _context.SaveChanges();
        }
        public void AddEntreprise(Entreprise entreprise)
        {
            _context.Entreprises.Add(entreprise);
            _context.SaveChanges();
        }
        public void ModifierEntreprise(Entreprise entreprise)
        {
            _context.Entry(entreprise).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Entreprise GetEntrepriseById(int id)
        {
            //int identifiant = int.Parse(id);
            return _context.Entreprises.Where(e => e.IdEntreprise == id).FirstOrDefault();
        }
        #region Pays
        public string GetIdPaysByMatriculePays(string libellePays)
        {
            var query = _context.Pays.Where(p => p.LibellePays.Equals(libellePays)).Select(p => p.Idpays2).SingleOrDefault();
            return query;
        }
        public List<Pays> GetAllPays()
        {
            return _context.Pays.ToList();
        }
        #endregion


        #region GetEntrepriseByDepartementEtOffre
        public List<Entreprise> GetEntrepriseByDepartementEtOffre(string produit, string departement)
        {
            List<Entreprise> query = _context.ProduitFormations
                                          .Where(pro => pro.LibelleProduitFormation.Contains(produit))
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
                                              IdEntreprise = pee.IdEntreprise
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
                                          c => c.IdEntreprise
                                           , ent => ent.IdEntreprise
                                           , (c, e) => new Entreprise
                                           {
                                               IdEntreprise = e.IdEntreprise,
                                               RaisonSociale = e.RaisonSociale,
                                               NumeroSiret = e.NumeroSiret,
                                               MailEntreprise = e.MailEntreprise,
                                               TelEntreprise = e.TelEntreprise,
                                               Ligne1Adresse = e.Ligne1Adresse,
                                               CodePostal = e.CodePostal,
                                               Ville = e.Ville,
                                               Idpays2 = e.Idpays2
                                           }).Where(e => e.CodePostal.StartsWith(departement)).ToList();
            return query;

        }
        public PagingList<Entreprise> GetEntrepriseByDepartementEtOffreForPaging(string produit, string departement, int page = 1)
        {
            var query = _context.ProduitFormations
                                         .Where(pro => pro.LibelleProduitFormation.Contains(produit))
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
                                             IdEntreprise = pee.IdEntreprise
                                         })

                                         .Join(_context.Entreprises,
                                         c => c.IdEntreprise
                                         , ent => ent.IdEntreprise
                                         , (c, e) => new Entreprise
                                         {
                                             IdEntreprise = e.IdEntreprise,
                                             RaisonSociale = e.RaisonSociale,
                                             NumeroSiret = e.NumeroSiret,
                                             MailEntreprise = e.MailEntreprise,
                                             TelEntreprise = e.TelEntreprise,
                                             Ligne1Adresse = e.Ligne1Adresse,
                                             CodePostal = e.CodePostal,
                                             Ville = e.Ville,
                                             Idpays2 = e.Idpays2
                                         }).Where(e => e.CodePostal.StartsWith(departement)).OrderBy(e => e.RaisonSociale);

            return PagingList.Create(query, 20, page);

        }
        #endregion


        #region GetEnterpriseByProduitFormation
        public List<Entreprise> GetEntrepriseByProduitFormation(string produit)
        {
            List<Entreprise> query2 = _context.ProduitFormations
                                          .Where(pro => pro.LibelleProduitFormation == produit)
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
                                              IdEntreprise = pee.IdEntreprise
                                           })
                                           .Join(_context.Contrats
                                           , b => b.matriculeBenef
                                           , c => c.MatriculeBeneficiaire
                                           , (b, c) => new
                                           {

                                               idEntrepretrise = c.IdEntreprise
                                           })
                                           .Join(_context.Entreprises,
                                          c => c.IdEntreprise
                                           , ent => ent.IdEntreprise
                                           , (c, e) => new Entreprise
                                           {
                                              IdEntreprise = e.IdEntreprise,
                                              RaisonSociale = e.RaisonSociale,
                                              NumeroSiret = e.NumeroSiret,
                                              MailEntreprise = e.MailEntreprise,
                                              TelEntreprise = e.TelEntreprise,
                                              Ligne1Adresse = e.Ligne1Adresse,
                                              CodePostal = e.CodePostal,
                                              Ville = e.Ville,
                                              Idpays2 = e.Idpays2
                                           }).ToList();

            return query2;
        }
        public PagingList<Entreprise> GetEntrepriseByProduitFormationForPaging(string produit, int page)
        {
            var query = _context.ProduitFormations
                                         .Where(pro => pro.LibelleProduitFormation == produit)
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
                                             IdEntreprise = pee.IdEntreprise
                                         })

                                         .Join(_context.Entreprises,
                                         c => c.IdEntreprise
                                         , ent => ent.IdEntreprise
                                         , (c, e) => new Entreprise
                                         {
                                             IdEntreprise = e.IdEntreprise,
                                             RaisonSociale = e.RaisonSociale,
                                             NumeroSiret = e.NumeroSiret,
                                             MailEntreprise = e.MailEntreprise,
                                             TelEntreprise = e.TelEntreprise,
                                             Ligne1Adresse = e.Ligne1Adresse,
                                             CodePostal = e.CodePostal,
                                             Ville = e.Ville,
                                             Idpays2 = e.Idpays2
                                         }).OrderBy(e => e.RaisonSociale);
            return PagingList.Create(query, 20, page);
        }
        #endregion

    }
}
