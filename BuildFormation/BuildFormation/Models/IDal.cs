using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public interface IDal : IDisposable
    {
        #region Ecole
        void CreerEcole(string nom, string adresse, string telephone, string email);
        bool ModifierEcole(int id,string nom, string adresse, string telephone, string email);
        bool SupprimerEcole(int id);
        Ecole ObtenirEcole(int id);
        Ecole ObtenirEcole(string nom);
        List<Ecole> ObtenirListeEcoles();
       List<Faculte> ObtenirListeFacultesDUnEcole(int idEcole);
        List<Faculte> ObtenirListeFacultesDUnEcole(Ecole ecole);
        #endregion

        #region Faculte

        void CreerFaculte(string nom,Ecole ecole);
        bool ModifierFaculte(int id, string nom);
        bool SupprimerFaculte(int id);
        Faculte ObtenirFaculte(int id);
        Faculte ObtenirFaculte(string nom);
        List<Faculte> ObtenirListeFacultes();

        #endregion


    }
}