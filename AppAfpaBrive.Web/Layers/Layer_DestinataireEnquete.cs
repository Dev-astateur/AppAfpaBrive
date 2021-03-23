using System;
using System.Collections.Generic;
using System.Text;
using AppAfpaBrive.BOL;
using System.Linq;

namespace AppAfpaBrive.DAL.Layers
{
    public class Layer_DestinataireEnquete
    {
        private readonly AFPANADbContext _db;
        public Layer_DestinataireEnquete(AFPANADbContext context)
        {
            _db = context; 
        }

        public DestinataireEnquete GetDestinataireEnqueteByIdSoumissionnaire(Guid guid)
        {
            DestinataireEnquete destinataireEnquete = new DestinataireEnquete();
            destinataireEnquete = _db.DestinataireEnquetes.Where(de => de.IdSoumissionnaire == guid).FirstOrDefault();
            return destinataireEnquete; 
        }
    }
}
