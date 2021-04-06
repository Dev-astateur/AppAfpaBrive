﻿using System;
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

        private int _Tuteur_create_Id;
        private int _Responsable_create_Id;

        private bool _Tuteur_create;
        private bool _Responsable_create;

        private string _Tuteur_AdresseMail;
        private string _Tuteur_Telephone;
        private string _Tuteur_Fonction;
        private int _Tuteur_genre;

        private string _Responsable_AdresseMail;
        private string _Responsable_Telephone;
        private string _Responsable_Fonction;
        private int _Responsable_genre;

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
        public int Tuteur_create_Id { get => _Tuteur_create_Id; set => _Tuteur_create_Id = value; }
        public int Responsable_create_Id { get => _Responsable_create_Id; set => _Responsable_create_Id = value; }
        public bool Responsable_create { get => _Responsable_create; set => _Responsable_create = value; }
        public bool Tuteur_create { get => _Tuteur_create; set => _Tuteur_create = value; }
        public string Tuteur_AdresseMail { get => _Tuteur_AdresseMail; set => _Tuteur_AdresseMail = value; }
        public string Tuteur_Telephone { get => _Tuteur_Telephone; set => _Tuteur_Telephone = value; }
        public string Tuteur_Fonction { get => _Tuteur_Fonction; set => _Tuteur_Fonction = value; }
        public string Responsable_AdresseMail { get => _Responsable_AdresseMail; set => _Responsable_AdresseMail = value; }
        public string Responsable_Telephone { get => _Responsable_Telephone; set => _Responsable_Telephone = value; }
        public string Responsable_Fonction { get => _Responsable_Fonction; set => _Responsable_Fonction = value; }
        public int Tuteur_genre { get => _Tuteur_genre; set => _Tuteur_genre = value; }
        public int Responsable_genre { get => _Responsable_genre; set => _Responsable_genre = value; }
    }
}
