using System;
using System.Collections.Generic;
using System.Text;

namespace AppAfpaBrive.DAL.Layer
{
    public class PaysLayer
    {
        private readonly AFPANADbContext _context = null;

        public PaysLayer( AFPANADbContext context )
        {
            _context = context;
        }
    }
}
