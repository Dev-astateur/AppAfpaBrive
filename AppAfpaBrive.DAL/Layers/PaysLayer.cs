using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.DAL.Layers
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

        public IEnumerable<Pays> GetAll()
        {
            return _context.Pays.ToList();
        }

        public IEnumerable<string> GetAllLibelle()
        {
            return _context.Pays.Select(x=>x.LibellePays).ToList();

           // return _context.Pays.Select(x=>x.LibellePays)
        }
    }
}
