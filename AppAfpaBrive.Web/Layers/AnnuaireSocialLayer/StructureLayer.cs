using AppAfpaBrive.BOL.AnnuaireSocial;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView.AnnuaireModelView;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers.AnnuaireSocialLayer
{
    public class StructureLayer
    {
        private readonly AFPANADbContext _context;

        public StructureLayer(AFPANADbContext context)
        {
            _context = context;
        }


        public async Task<PagingList<Structure>> GetPage(string filter, int page = 1, string sortExpression = "NomStructure")
        {
            var qry = _context.Structures.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.NomStructure.Contains(filter));
            }

            return await PagingList.CreateAsync<Structure>(qry, 20, page, sortExpression, "NomStructure");
        }

        public StructureModelView GetStructureById(int id)
        {
            var obj = _context.Structures.Select(e => new StructureModelView
            {
                IdStructure = e.IdStructure,
                NomStructure = e.NomStructure,
                LigneAdresse1 = e.LigneAdresse1,
                LigneAdresse2 = e.LigneAdresse2,
                LigneAdresse3 = e.LigneAdresse3,
                CodePostal = e.CodePostal,
                Ville = e.Ville,
                Mail = e.Mail,
                Telephone = e.Telephone
            }).FirstOrDefault(s => s.IdStructure == id);

            return obj as StructureModelView;
        }

        public void Insert(Structure structure)
        {
            _context.Structures.Add(structure);
            _context.SaveChanges();
        }

        public void Delete(Structure structure)
        {
            _context.Structures.Remove(structure);
            _context.SaveChanges();
        }

        public void Update(Structure structure)
        {

        }

    }
}
