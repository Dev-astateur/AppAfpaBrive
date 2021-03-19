using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView;
using AppAfpaBrive.BOL;

namespace Projet_Test
{
    public class TestProduitDeFormation
    {
        [Test]
        public void LibelleCourtFormationTropLong()
        {
            ProduitFormation produit = new ProduitFormation
            {
                CodeProduitFormation = 6,
                NiveauFormation= "3",
                LibelleCourtFormation = "azerazerzee"
            };
            //Assert.IsFalse(produit.IsValid);
        }

        [Test]
        public void LibelleCourtFormationValide()
        {
            ProduitFormationModelView produit = new ProduitFormationModelView
            {
                CodeProduitFormation = 6,
                NiveauFormation = "3",
                LibelleCourtFormation = "azerazerzee"
            };
            //Assert.IsTrue(produit.IsValid);
        }
    }
}
