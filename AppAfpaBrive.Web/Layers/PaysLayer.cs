﻿using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.Web.Layers
{
    public class PaysLayer
    {
        private readonly AFPANADbContext _context = null;

        public PaysLayer( AFPANADbContext context )
        {
            _context = context;
        }

        public Pays GetPaysById(string idPays)
        {
            return _context.Pays.Find(idPays);
        }
    }
}