using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.Formateur
{
    [Route("api/chargerStagiaires")]
    [ApiController]
    public class ChargerStagiairesController : ControllerBase
    {
        private AFPANADbContext _context;

        public ChargerStagiairesController(AFPANADbContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var sta = _context.OffreFormations
                    .Where(x => x.LibelleOffreFormation == term)
                    .Join(_context.BeneficiaireOffreFormations
                    , p => p.IdOffreFormation
                    , b => b.IdOffreFormation
                    , (p, b) => new
                    {
                        matricule = b.MatriculeBeneficiaire
                    })
                    .Join(_context.Beneficiaires
                    , be => be.matricule
                    , b => b.MatriculeBeneficiaire
                    , (be, b) => new Beneficiaire
                    {
                        MatriculeBeneficiaire = b.MatriculeBeneficiaire,
                        CodeTitreCivilite = b.CodeTitreCivilite,
                        NomBeneficiaire = b.NomBeneficiaire,
                        PrenomBeneficiaire = b.PrenomBeneficiaire,
                        DateNaissanceBeneficiaire = b.DateNaissanceBeneficiaire,
                        MailBeneficiaire = b.MailBeneficiaire,
                        TelBeneficiaire = b.TelBeneficiaire,
                        Ligne1Adresse = b.Ligne1Adresse,
                        Ligne2Adresse = b.Ligne2Adresse,
                        Ligne3Adresse = b.Ligne3Adresse,
                        CodePostal = b.CodePostal,
                        Ville = b.Ville,
                        UserId = b.UserId,
                        IdPays2 = b.IdPays2,
                        PathPhoto = b.PathPhoto,
                        MailingAutorise = b.MailingAutorise
                    })
                    .OrderBy(x => x.NomBeneficiaire).ToList();

                return Ok(sta);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

