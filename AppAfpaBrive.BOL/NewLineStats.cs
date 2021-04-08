using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL
{
    public class NewLineStats : IInsertion
    {
        public string IdEtablissement { get; set; }
        public int IdOffreFormation { get; set; }
        public int Annee { get; set; }
        public bool EnLienAvecFormation { get; set; }
        public int TotalReponse { get; set; }
        public int Cdi { get; set; }
        public int Cdd { get; set; }
        public int Alternance { get; set; }
        public int SansEmploie { get; set; }
        public int Autres { get; set; }

        public InsertionsTroisMois ToInsertionTroisMois()
        {
            return new InsertionsTroisMois
            {
                IdEtablissement = IdEtablissement,
                IdOffreFormation = IdOffreFormation,
                Annee = Annee,
                EnLienAvecFormation = EnLienAvecFormation,
                TotalReponse = TotalReponse,
                Cdi = Cdi,
                Cdd = Cdd,
                Alternance = Alternance,
                SansEmploie = SansEmploie,
                Autres = Autres
            };
        }

        public InsertionsSixMois ToInsertionSixMois()
        {
            return new InsertionsSixMois
            {
                IdEtablissement = IdEtablissement,
                IdOffreFormation = IdOffreFormation,
                Annee = Annee,
                TotalReponse = TotalReponse,
                EnLienAvecFormation = EnLienAvecFormation,
                Cdi = Cdi,
                Cdd = Cdd,
                Alternance = Alternance,
                SansEmploie = SansEmploie,
                Autres = Autres
            };
        }

        public InsertionsDouzeMois ToInsertionDouzeMois()
        {
            return new InsertionsDouzeMois
            {
                IdEtablissement = IdEtablissement,
                IdOffreFormation = IdOffreFormation,
                Annee = Annee,
                EnLienAvecFormation = EnLienAvecFormation,
                TotalReponse = TotalReponse,
                Cdi = Cdi,
                Cdd = Cdd,
                Alternance = Alternance,
                SansEmploie = SansEmploie,
                Autres = Autres
            };
        }

        public bool IsValid()
        {
            return IdEtablissement != null && IdOffreFormation != 0 && Annee != 0;
        }
    }
}
