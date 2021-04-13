using System;
using System.Collections.Generic;
using System.Text;
using AppAfpaBrive.BOL;
using System.Linq;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_TypeContrat
    {
        private readonly AFPANADbContext _db;
        public Layer_TypeContrat(AFPANADbContext context)
        {
            _db = context;
        }

        public TypeContrat GetTypeContratById(int id)
        {
            return _db.TypeContrats.Where(tc => tc.IdTypeContrat == id).FirstOrDefault();
        }
    }

}
