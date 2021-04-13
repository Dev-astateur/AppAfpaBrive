using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers
{
    public partial class Layer_Pee
    {
        public class Layer_PeeDocument
        {
            private readonly AFPANADbContext _db;
            public Layer_PeeDocument(AFPANADbContext context)
            {
                _db = context;
            }

            public void create(PeeDocument peeDocument)
            {
                _db.PeeDocuments.Add(peeDocument);
                _db.SaveChanges();
            }
        }

    }
}