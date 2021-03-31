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

        public List<string> SearchLibellesAppellationsRome(string userInput)
        {
            return _db.AppelationRomes.Where(ar => ar.LibelleAppelationRome.Contains(userInput)).Select(ar=>ar.LibelleAppelationRome).ToList();
        }

        public List<AppelationRome> SearchApellationsRome(string userInput)
        {
            return _db.AppelationRomes.Where(ar => ar.LibelleAppelationRome.Contains(userInput)).ToList(); 
        }

        public AppelationRome GetAppellationRomeByLibelle(string LibelleAppellationRome)
        {
            return _db.AppelationRomes.Where(ar => ar.LibelleAppelationRome == LibelleAppellationRome).FirstOrDefault(); 
        }
    }
}
