using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;

namespace AppAfpaBrive.Web.ModelView
{
    public class PeeComparer : IEqualityComparer<Pee>
    {
        #region Méthode Equal
        /// <summary>
        /// Les Pee sont égaux si les noms et Matricules des Beneficiaires sont égaux.
        /// </summary>
        /// <param name="PeeBeneficiaire1"></param>
        /// <param name="PeeBeneficiaire2"></param>
        /// <returns></returns>
        public bool Equals(Pee PeeBeneficiaire1, Pee PeeBeneficiaire2)
        {
            //Vérifie si les objets comparés font référence aux mêmes données.
            if (object.ReferenceEquals(PeeBeneficiaire1, PeeBeneficiaire2)) return true;
            // Vérifie si l'un des objets comparés est nul.
            if (ReferenceEquals(PeeBeneficiaire1, null) || ReferenceEquals(PeeBeneficiaire2, null)) return false;
            // Vérifie si les propriétés Pee sont égales.
            return PeeBeneficiaire1.MatriculeBeneficiaire == PeeBeneficiaire2.MatriculeBeneficiaire 
                && PeeBeneficiaire1.MatriculeBeneficiaireNavigation.NomBeneficiaire == PeeBeneficiaire2.MatriculeBeneficiaireNavigation.NomBeneficiaire;


        }
        #endregion
        #region Méthode GetHashCode
        /// <summary>
        /// si la méthode Equals() renvoie true pour une paire d'objets
        /// alors GetHashCode() doit retourner la même valeur pour ces objets.
        /// </summary>
        /// <param name="pee"></param>
        /// <returns></returns>
        public int GetHashCode([DisallowNull] Pee pee)
        {
            // Vérifie si l'objet est nul
            if (Object.ReferenceEquals(pee, null)) return 0;
            // Récupère le hash code  pour le champ MatriculeBenenificiaire s'il n'est pas nul.
            int hashMatriculeBeneficiaire = pee.MatriculeBeneficiaire == null ? 0 : pee.MatriculeBeneficiaire.GetHashCode();
            // Récupère le code de hachage pour le champ NomBeneficiaire.
            int hashNomBeneficiaire = pee.MatriculeBeneficiaireNavigation.NomBeneficiaire.GetHashCode();
            // Calcule le code de hachage de l'objet pee.
            return hashMatriculeBeneficiaire ^ hashNomBeneficiaire;

        }
        #endregion
    }
}
