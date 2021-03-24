using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
{
    public class CollaborateurAfpaLayer
    {

        private readonly AFPANADbContext _context;

        public CollaborateurAfpaLayer(AFPANADbContext context)
        {
            _context = context;
        }

        // Récupère les formateurs selon le début du nom
        public List<CollaborateurAfpa> GetCollaborateurStartWith(string name)
        {
            return _context.CollaborateurAfpas.Where(x => x.NomCollaborateur.StartsWith(name)).ToList();
        }

    }
}
