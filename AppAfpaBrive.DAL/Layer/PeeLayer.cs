using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Models.Layout
{
    public class PeeLayer
    {
        private readonly AFPANADbContext _dbContext = null;

        public PeeLayer (AFPANADbContext context) 
        {
            _dbContext = context;
        }


    }
}
