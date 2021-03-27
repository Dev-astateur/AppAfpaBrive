using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL; 

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_AppelationRomes
    {
        private readonly AFPANADbContext _db;
        public Layer_AppelationRomes(AFPANADbContext context)
        {
            _db = context;
        }

        public List<string> GetAllLibelleAppelationRomes()
        {
            return _db.AppelationRomes.Select(ar => ar.LibelleAppelationRome).ToList(); 
        }
    }
}
