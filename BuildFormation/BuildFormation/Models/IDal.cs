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
        List<Filiere> ObtenirListeFileresDUnFaculte(Faculte faculte);
        #endregion

        #region Filiere

        void CreerFiliere(string nom, Faculte faculte);
        bool ModifierFiliere(int id, string nom);
        bool SupprimerFiliere(int id);
        Filiere ObtenirFiliere(int id);
        Filiere ObtenirFiliere(string nom);
        
        List<Filiere> ObtenirListeFilieres();

        List<Option> ObtenirListeOptionesDUnFaculte(Filiere filiere);


        #endregion


        #region Option

        void CreerOption(string nom, Filiere filiere);
        bool ModifierOption(int id, string nom);
        bool SupprimerOption(int id);
        Option ObtenirOption(int id);
        Option ObtenirOption(string nom);

        List<Option> ObtenirListeOptions();

        List<Specialite> ObtenirListeSpecialitesDeLOption(Option option);


        #endregion

        #region Specialite

        void CreerSpecialite(string nom, Option option);
        bool ModifierSpecialite(int id, string nom);
        bool SupprimerSpecialite(int id);
        Specialite ObtenirSpecialite(int id);
        Specialite ObtenirSpecialite(string nom);

        List<Specialite> ObtenirListeSpecialites();

        #endregion


    }
}