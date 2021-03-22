using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Models
{
    public class Creation_convention
    {
        private string _idEtablissement;
        private string _etablissement;
        private int _idFormation;
        private string _idmatricule;
        private DateTime _dateDebut;
        private DateTime? _datefin;
        private string _formation;
        private string _Siret;
        private int _IdEntreprise;
        private int _idTuteur;
        private int _idResponsable;
        private string _tuteurPrenom;
        private string _ResponsablePrenom;
        private string _tuteurNom;
        private string _ResponsableNom;
        private string _raison_social;



        public string IdEtablissement { get => _idEtablissement; set => _idEtablissement = value; }
        public string Etablissement { get => _etablissement; set => _etablissement = value; }
        public int IdFormation { get => _idFormation; set => _idFormation = value; }
        public string Idmatricule { get => _idmatricule; set => _idmatricule = value; }
        public DateTime DateDebut { get => _dateDebut; set => _dateDebut = value; }
        public DateTime? Datefin { get => _datefin; set => _datefin = value; }
        
        public string Formation { get => _formation; set => _formation = value; }
        public string Siret { get => _Siret; set => _Siret = value; }
        public int IdEntreprise { get => _IdEntreprise; set => _IdEntreprise = value; }
        public int IdTuteur { get => _idTuteur; set => _idTuteur = value; }
        public int IdResponsable { get => _idResponsable; set => _idResponsable = value; }
        public string TuteurPrenom { get => _tuteurPrenom; set => _tuteurPrenom = value; }
        public string ResponsablePrenom { get => _ResponsablePrenom; set => _ResponsablePrenom = value; }
        public string TuteurNom { get => _tuteurNom; set => _tuteurNom = value; }
        public string ResponsableNom { get => _ResponsableNom; set => _ResponsableNom = value; }
        public string Raison_social { get => _raison_social; set => _raison_social = value; }
    }
}
