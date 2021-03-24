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

        private int _idTuteur;
        private int _idResponsable;
        private string _tuteurPrenom;
        private string _ResponsablePrenom;
        private string _tuteurNom;
        private string _ResponsableNom;


        private string _Siret;
        private int _IdEntreprise;
        private string _Entreprise_raison_social;
        private string _Entreprise_codePostal;
        private string _Entreprise_Ligne1Adresse;
        private string _Entreprise_Ligne2Adresse;
        private string _Entreprise_Ligne3Adresse;
        private string _Entreprise_Ville;
        private string _Entreprise_IdPays;
        private string _Entreprise_Mail;
        private string _Entreprise_Tel;
        private bool _Entreprise_Create;




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
        public string Entreprise_raison_social { get => _Entreprise_raison_social; set => _Entreprise_raison_social = value; }
        public string Entreprise_codePostal { get => _Entreprise_codePostal; set => _Entreprise_codePostal = value; }
        public string Entreprise_Ligne1Adresse { get => _Entreprise_Ligne1Adresse; set => _Entreprise_Ligne1Adresse = value; }
        public string Entreprise_Ligne2Adresse { get => _Entreprise_Ligne2Adresse; set => _Entreprise_Ligne2Adresse = value; }
        public string Entreprise_Ligne3Adresse { get => _Entreprise_Ligne3Adresse; set => _Entreprise_Ligne3Adresse = value; }
        public string Entreprise_Ville { get => _Entreprise_Ville; set => _Entreprise_Ville = value; }
        public string Entreprise_IdPays { get => _Entreprise_IdPays; set => _Entreprise_IdPays = value; }
        public string Entreprise_Mail { get => _Entreprise_Mail; set => _Entreprise_Mail = value; }
        public string Entreprise_Tel { get => _Entreprise_Tel; set => _Entreprise_Tel = value; }
        public bool Entreprise_Create { get => _Entreprise_Create; set => _Entreprise_Create = value; }
    }
}
