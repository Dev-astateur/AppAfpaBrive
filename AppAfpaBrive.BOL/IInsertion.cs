namespace AppAfpaBrive.BOL
{
    public interface IInsertion
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


        public bool IsValid()
        {
            return IdEtablissement != null && IdOffreFormation != 0 && Annee != 0 && (EnLienAvecFormation == true || EnLienAvecFormation == false);
        }


    }
}