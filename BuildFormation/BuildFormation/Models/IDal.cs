using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public interface IDal : IDisposable
    {
        #region Ecole
        Ecole CreerEcole(string nom, string adresse, string telephone, string email);
        bool ModifierEcole(int id,string nom, string adresse, string telephone, string email);
        bool SupprimerEcole(int id);
        Ecole ObtenirEcole(int id);
        Ecole ObtenirEcole(string nom);
        List<Ecole> ObtenirListeEcoles();
         List<Faculte> ObtenirListeFacultesDUnEcole(Ecole ecole);
        #endregion

        #region Faculte

        Faculte CreerFaculte(string nom,Ecole ecole);
        bool ModifierFaculte(int id, string nom);
        bool SupprimerFaculte(int id);
        Faculte ObtenirFaculte(int id);
        Faculte ObtenirFaculte(string nom);
        List<Faculte> ObtenirListeFacultes();
        List<Filiere> ObtenirListeFileresDUnFaculte(Faculte faculte);
        #endregion

        #region Filiere

        Filiere CreerFiliere(string nom, Faculte faculte);
        bool ModifierFiliere(int id, string nom);
        bool SupprimerFiliere(int id);
        Filiere ObtenirFiliere(int id);
        Filiere ObtenirFiliere(string nom);
        
        List<Filiere> ObtenirListeFilieres();

        List<Option> ObtenirListeOptionesDUnFiliere(Filiere filiere);


        #endregion


        #region Option

        Option CreerOption(string nom, Filiere filiere);
        bool ModifierOption(int id, string nom);
        bool SupprimerOption(int id);
        Option ObtenirOption(int id);
        Option ObtenirOption(string nom);

        List<Option> ObtenirListeOptions();

        List<Specialite> ObtenirListeSpecialitesDeLOption(Option option);


        #endregion

        #region Specialite

        Specialite CreerSpecialite(string nom, Option option);
        bool ModifierSpecialite(int id, string nom);
        bool SupprimerSpecialite(int id);
        Specialite ObtenirSpecialite(int id);
        Specialite ObtenirSpecialite(string nom);

        List<Specialite> ObtenirListeSpecialites();
        List<Membre> ObtenirListeMembreDuSpecialite(Specialite specialite);

        #endregion

        #region Membre

        Membre CreerMembre(string nom, string prenom,string pseudo, string adresse, string email, Privilege privilege,string motDePasse, Specialite specialite);

        bool ModifierMembre(int id, string nom, string prenom, string pseudo, string adresse, string email, Privilege privilege, string motDePasse, Specialite specialite);
         Membre ObtenirMembre(int id);
        List<Membre> ObtenirListeMembres();

        Membre Authentifier(string pseudoOuAdresseEmail, string motDePasse);
        bool SupprimerMembre(int id);
        string EncodeMd5(string motDePasse);

        #endregion

        #region Topic

        Topic CreerTopicTopic(string titre, string contenu, Membre auteur, string theme, DateTime dateDePublication);
        bool ModifierTopic(int id,string titre, string contenu, string theme);
        Topic ObtenirTopic(int id);
        List<Topic> ObtenirListeTopics();

        bool SupprimerTopic(int id);


        #endregion


    }
}