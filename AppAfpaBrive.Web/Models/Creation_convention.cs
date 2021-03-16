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
        private string _responsableJuridique;
        private string _tureur;
        private string _formation;

        public string IdEtablissement { get => _idEtablissement; set => _idEtablissement = value; }
        public string Etablissement { get => _etablissement; set => _etablissement = value; }
        public int IdFormation { get => _idFormation; set => _idFormation = value; }
        public string Idmatricule { get => _idmatricule; set => _idmatricule = value; }
        public DateTime DateDebut { get => _dateDebut; set => _dateDebut = value; }
        public DateTime? Datefin { get => _datefin; set => _datefin = value; }
        public string ResponsableJuridique { get => _responsableJuridique; set => _responsableJuridique = value; }
        public string Tureur { get => _tureur; set => _tureur = value; }
        public string Formation { get => _formation; set => _formation = value; }
    }
}
