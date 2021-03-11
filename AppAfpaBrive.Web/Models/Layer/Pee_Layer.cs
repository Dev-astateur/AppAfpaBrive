using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Models.Layout
{
    public class Pee_Layer
    {
        private readonly AFPANADbContext _dbContext = null;

        public Pee_Layer (AFPANADbContext context) 
        {
            _dbContext = context;
        }
    }
}
