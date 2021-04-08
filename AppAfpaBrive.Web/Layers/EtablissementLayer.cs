﻿using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
{
    public class EtablissementLayer
    {
        private readonly AFPANADbContext _context;

        #region Constructeur
        public EtablissementLayer(AFPANADbContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<PagingList<Etablissement>> GetPage(string filter, int page = 1, string sortExpression = "NomEtablissement")
        {
            var qry = _context.Etablissements.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.NomEtablissement.Contains(filter));
            }

            return await PagingList.CreateAsync<Etablissement>(qry, 20, page, sortExpression, "NomEtablissement");
        }

        public void InsertProduit(Etablissement prod)
        {
            var check = CheckIdEtablissementExiste(prod.IdEtablissement);
            if (check == true)
            {
                _context.Etablissements.Add(prod);
                _context.SaveChanges();
            }
        }
        public void Remove(Etablissement prod)
        {
            _context.Etablissements.Remove(prod);
            _context.SaveChanges();
        }
        public void Update(Etablissement prod)
        {
            _context.Etablissements.Update(prod);
            _context.SaveChanges();
        }
        public EtablissementModelView GetByIdEtablissement(string idEtablissement)
        {
            var obj = _context.Etablissements.Select(e => new EtablissementModelView()
            {
                IdEtablissement =e.IdEtablissement,
                IdEtablissementRattachement = e.IdEtablissementRattachement,
                NomEtablissement = e.NomEtablissement,
                MailEtablissement=e.MailEtablissement,
                TelEtablissement = e.TelEtablissement,
                Ligne1Adresse = e.Ligne1Adresse,
                Ligne2Adresse = e.Ligne2Adresse,
                Ligne3Adresse=e.Ligne3Adresse,
                CodePostal = e.CodePostal,
                Ville = e.Ville
            }).First(p => p.IdEtablissement == idEtablissement);
            return obj as EtablissementModelView;
        }

        public Etablissement GetByIdEtablissementDelete(string idEtablissement)
        {
            return _context.Etablissements.Find(idEtablissement);
        }

        public bool CheckIdEtablissementExiste(string id)
        {
            var idetablissement = _context.Etablissements.Find(id);

            if (idetablissement == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// retourne des informations d'un etablissement
        /// dans un objet de type EtablissementModelView du répertoire Accueil
        /// </summary>
        /// <param name="id">id de etablissement</param>
        /// <returns></returns>
        public async Task<AccueilModelView> GetEtablissementByIdAsync(string id)
        {
            return await _context.Etablissements.Where(e => e.IdEtablissement == id).Select(e => new AccueilModelView()
            {
                IdEtablissement = e.IdEtablissement,
                NomEtablissement = e.NomEtablissement,
                TelEtablissement = e.TelEtablissement
            }).FirstOrDefaultAsync();
        }
    }
}