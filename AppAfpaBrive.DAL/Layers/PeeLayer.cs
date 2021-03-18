﻿using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;

namespace AppAfpaBrive.DAL.Layers
{
    public class PeeLayer
    {
        private readonly AFPANADbContext _dbContext = null;

        public PeeLayer (AFPANADbContext context) 
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Pee>> GetPeeByMatriculeCollaborateurAfpaAsync(string idMatricule)
        {
            return await _dbContext.Pees.Where(e => e.Id.MatriculeCollaborateurAfpa == idMatricule && e.Etat == 0)
                .Include(e => e.Id)
                .Include(e=>e.IdEntrepriseNavigation)
                .Include(e => e.MatriculeBeneficiaireNavigation).ToListAsync();
        }

        public async Task<Pee> GetPeeByIdPeeOffreEntreprisePaysAsync(int idPee)
        {
            return await _dbContext.Pees.Where(e=>e.IdPee==idPee)
                .Include(e => e.Id)
                .Include(e=>e.IdEntrepriseNavigation).ThenInclude(e=>e.Idpays2Navigation)
                .FirstOrDefaultAsync();
        }

        public async Task<Pee> GetPeeByIdAsync(int idPee)
        {
            return await _dbContext.Pees.FindAsync((decimal)idPee);
        }
    }
}
