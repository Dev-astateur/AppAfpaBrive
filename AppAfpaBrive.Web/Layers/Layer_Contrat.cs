using System;
using System.Collections.Generic;
using System.Text;
using AppAfpaBrive.BOL;
using System.Linq;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_Contrat
    {
        private readonly AFPANADbContext _db;
        public Layer_Contrat(AFPANADbContext context)
        {
            _db = context;
        }

        public Contrat GetContratByIdContrat(int? id)
        {
           Contrat contrat = _db.Contrats.Where(c => c.IdContrat == id).FirstOrDefault();
           return contrat; 
        }
    }
}
