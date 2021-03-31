using System;
using System.Collections.Generic;
using System.Text;
using AppAfpaBrive.BOL;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppAfpaBrive.DAL.Layers
{
    public class Layer_TypeContrat
    {
        private readonly AFPANADbContext _db;
        public Layer_TypeContrat(AFPANADbContext context)
        {
            _db = context;
        }

        public IEnumerable<SelectListItem> GetAllToDropDownList()
        {
            return _db.TypeContrats
                .Select(tc => new SelectListItem
                {
                    Text = tc.DesignationTypeContrat,
                    Value = tc.IdTypeContrat
                .ToString()
                })
                .ToList();
        }

        public TypeContrat GetTypeContratById(int id)
        {
            return _db.TypeContrats.Where(tc => tc.IdTypeContrat == id).FirstOrDefault();
        }

        public TypeContrat GetTypeContratByDesignation(string designation)
        {
            return _db.TypeContrats.Where(tc => tc.DesignationTypeContrat == designation).FirstOrDefault();
        }

        public List<string> GetDesignationsTypeContrat()
        {
            return _db.TypeContrats.Select(tc => tc.DesignationTypeContrat).ToList(); 
        }

        public int GetIdTypeContratByDesignation(string desgignation)
        {
            return _db.TypeContrats.Where(tc => tc.DesignationTypeContrat == desgignation).Select(tc => tc.IdTypeContrat).FirstOrDefault(); 
        }
    }

}
