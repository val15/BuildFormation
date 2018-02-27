using System;
using System.Collections.Generic;
using System.Data.Entity;
using BuildFormation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildFormation.Tests
{
    [TestClass]
    public class DalTests
    {
        private IDal _dal;

        [TestInitialize] //cette "décoration" permet de dénir une methode qui sera executée avant chaque teste
        public void Init_AvantChaqueTest()
        {
            //reinitialisation de la base avant les teste
            IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new BddContext());

            _dal = new Dal();
        }

        #region Ecole
        [TestMethod]
        public void CreerEcole_AvecUnNouvelEcole_ObtientTousLesEcolesRenvoitBienLEcole()
        {

            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            List<Ecole> ecloles = _dal.ObtenirListeEcoles();
            Assert.IsNotNull(ecloles);
            Assert.AreEqual(1, ecloles.Count);
            Assert.AreEqual("IFT", ecloles[0].Nom);
            Assert.AreEqual("LOT 2I39A Ampandrana", ecloles[0].Adresse);
            Assert.AreEqual("0330257032", ecloles[0].Telephone);
            Assert.AreEqual("ift@gmail.com", ecloles[0].Email);

        }


        [TestMethod]
        public void
        ModifierEcole_CreerUnNouvelEcole_ObtenirCetEcole_ModifierCetEcole_ObtientTousLesEcolesRenvoitBienLEcoleModifier()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole(1);
            Assert.AreEqual("IFT", ecole.Nom);
            _dal.ModifierEcole(ecole.Id, "IFTModif", "LOT 2I39A AmpandranaModif", "000000000", "ift@gmail.comModif");
            ecole = _dal.ObtenirEcole(1);
            Assert.AreEqual("IFTModif", ecole.Nom);
            Assert.AreEqual("LOT 2I39A AmpandranaModif", ecole.Adresse);
            Assert.AreEqual("000000000", ecole.Telephone);
            Assert.AreEqual("ift@gmail.comModif", ecole.Email);
        }

        [TestMethod]
        public void SupprimerEcole_CreerUnNouvelEcole_ObtenirCetEcole_SupprimerCetEcole_OntenirCetEcol_RenvoitBienNULL()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");
            _dal.SupprimerEcole(ecole.Id);
            ecole = _dal.ObtenirEcole(1);
            Assert.IsNull(ecole);
        }

        [TestMethod]
        public void AttribuerDesFaculteesAUnEcole_CreerUnEcole_CreerDeuxFaculterAvecLEcole_OntenirLesFacultesDeLEcole_RenvoiBienLesFacultes()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");

            _dal.CreerFaculte("Science", ecole);
            _dal.CreerFaculte("Droit", ecole);
            ecole = _dal.ObtenirEcole("IFT");

            var facultesDeLEcole = _dal.ObtenirListeFacultesDUnEcole(ecole);
            Assert.IsNotNull(facultesDeLEcole);

            Assert.AreEqual(2, facultesDeLEcole.Count);
            Assert.AreEqual("Science", facultesDeLEcole[0].Nom);
            Assert.AreEqual("Droit", facultesDeLEcole[1].Nom);
        }


        #endregion


        #region Faculter
        [TestMethod]
        public void CreerFaculte_AvecUnNouveEcole_EtUnNouveuFaculte_ObtientTousLesFacultesRenvoitBienLeFaculte()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");

            _dal.CreerFaculte("Science", ecole);
            var facultes = _dal.ObtenirListeFacultes();
            Assert.IsNotNull(facultes);
            Assert.AreEqual(1, facultes.Count);
            Assert.AreEqual("Science", facultes[0].Nom);
        }

        [TestMethod]
        public void ModificerSupprimerFaculte_AvecDeuxNouvauxFaculte_ObtientTousLesFacultes_ModifierLePremier_SupprimerLeSecond_RenvoitBienLaModificationEtNullSurLeSupprimer()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");

            _dal.CreerFaculte("Scien", ecole);
            _dal.CreerFaculte("Test", ecole);
            List<Faculte> facultes = _dal.ObtenirListeFacultes();
            var premier = _dal.ObtenirFaculte(facultes[0].Id);
            var second = _dal.ObtenirFaculte(facultes[1].Nom);
            Assert.IsNotNull(facultes);
            Assert.AreEqual(2, facultes.Count);
            Assert.AreEqual("Scien", premier.Nom);
            Assert.AreEqual("Test", second.Nom);
            _dal.ModifierFaculte(premier.Id, "Science");
            _dal.SupprimerFaculte(second.Id);
            Assert.AreEqual("Science", premier.Nom);
            second = _dal.ObtenirFaculte(2);
            Assert.IsNull(second);
        }

        [TestMethod]
        public void AttribuerDesFiliereAUnFaculte_CreerUnEcole_CreerUnFaculterAvecLEcole_CreerDeuxFilierePourLaFaculte_OntenirLesFilieresDeLaFaculte_RenvoiBienLesFilieres()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");
            Assert.IsNotNull(ecole);
            var faculte = _dal.CreerFaculte("Science", ecole);
            Assert.IsNotNull(faculte);

            _dal.CreerFiliere("F1", faculte);
            _dal.CreerFiliere("F2", faculte);

            var filieresDuFaculte = _dal.ObtenirListeFileresDUnFaculte(faculte);
            Assert.IsNotNull(filieresDuFaculte);

            Assert.AreEqual(2, filieresDuFaculte.Count);
            Assert.AreEqual("F1", filieresDuFaculte[0].Nom);
            Assert.AreEqual("F2", filieresDuFaculte[1].Nom);

        }

        #endregion

        #region Filiere

        [TestMethod]
        public void CreerFiliere_AvecUnNouveEcole_EtUnNouveuFaculte_EtUnNouveauFiliere_ObtientTousLesFilieresRenvoitBienLeFiliere()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");

            _dal.CreerFaculte("Science", ecole);
            var faculte = _dal.ObtenirFaculte("Sience");
            _dal.CreerFiliere("Mathematique", faculte);


            var filieres = _dal.ObtenirListeFilieres();
            Assert.IsNotNull(filieres);
            Assert.AreEqual(1, filieres.Count);
            Assert.AreEqual("Mathematique", filieres[0].Nom);
        }

        [TestMethod]
        public void ModificerSupprimerFiliere_AvecDeuxNouvauxFaculte_ObtientTousLesFacultes_ModifierLePremier_SupprimerLeSecond_RenvoitBienLaModificationEtNullSurLeSupprimer()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");
            _dal.CreerFaculte("Scien", ecole);
            var faculte = _dal.ObtenirFaculte(1);
            _dal.CreerFiliere("F1", faculte);
            _dal.CreerFiliere("F2", faculte);
            var filieres = _dal.ObtenirListeFilieres();
            var premier = _dal.ObtenirFiliere(filieres[0].Id);
            var second = _dal.ObtenirFiliere(filieres[1].Nom);
            Assert.IsNotNull(filieres);
            Assert.AreEqual(2, filieres.Count);
            Assert.AreEqual("F1", premier.Nom);
            Assert.AreEqual("F2", second.Nom);
            _dal.ModifierFiliere(premier.Id, "Mathematique");
            _dal.SupprimerFiliere(second.Id);
            Assert.AreEqual("Mathematique", premier.Nom);
            second = _dal.ObtenirFiliere(2);
            Assert.IsNull(second);
        }

        [TestMethod]
        public void AttribuerDesOptionAUnFiliere_CreerUnEcole_CreerUnFaculterAvecLEcole_CreerUnFilierePourLaFaculte_CreerDeuxOption_OntenirLesOptionsDeLaFiliere_RenvoiBienLesOptions()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");
            _dal.CreerFaculte("Science", ecole);
            var faculte = _dal.ObtenirFaculte("Science");
            _dal.CreerFiliere("Mathematique", faculte);
            var filiere = _dal.ObtenirFiliere("Mathematique");

            _dal.CreerOption("O1", filiere);
            _dal.CreerOption("O2", filiere);

            var optionsDuFiliere = _dal.ObtenirListeOptionesDUnFiliere(filiere);
            Assert.IsNotNull(optionsDuFiliere);

            Assert.AreEqual(2, optionsDuFiliere.Count);
            Assert.AreEqual("O1", optionsDuFiliere[0].Nom);
            Assert.AreEqual("O2", optionsDuFiliere[1].Nom);

        }

        #endregion


        #region Option

        [TestMethod]
        public void CreerOption_AvecUnNouveEcole_EtUnNouveuFaculte_EtUnNouveauFiliere_EtUnNouveauOption_ObtientTousLesOptionsRenvoitBienLOption()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");
            _dal.CreerFaculte("Science", ecole);
            var faculte = _dal.ObtenirFaculte("Sience");
            _dal.CreerFiliere("Mathematique", faculte);
            var filiere = _dal.ObtenirFiliere("Mathematique");

            _dal.CreerOption("Mecanique", filiere);
            var options = _dal.ObtenirListeOptions();
            Assert.IsNotNull(options);
            Assert.AreEqual(1, options.Count);
            Assert.AreEqual("Mecanique", options[0].Nom);
        }

        [TestMethod]
        public void ModificerSupprimerOption_AvecDeuxNouvauxOption_ObtientTousLesOptions_ModifierLePremier_SupprimerLeSecond_RenvoitBienLaModificationEtNullSurLeSupprimer()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");
            _dal.CreerFaculte("Scien", ecole);
            var faculte = _dal.ObtenirFaculte(1);
            _dal.CreerFiliere("Mathematique", faculte);
            var filiere = _dal.ObtenirFiliere("Mathematique");

            _dal.CreerOption("O1", filiere);
            _dal.CreerOption("O2", filiere);
            var options = _dal.ObtenirListeOptions();
            var premier = _dal.ObtenirOption(options[0].Id);
            var second = _dal.ObtenirOption(options[1].Nom);
            Assert.IsNotNull(options);
            Assert.AreEqual(2, options.Count);
            Assert.AreEqual("O1", premier.Nom);
            Assert.AreEqual("O2", second.Nom);
            _dal.ModifierOption(premier.Id, "Macanique");
            _dal.SupprimerOption(second.Id);
            Assert.AreEqual("Macanique", premier.Nom);
            second = _dal.ObtenirOption(2);
            Assert.IsNull(second);
        }

        [TestMethod]
        public void AttribuerDesSpecialitesAUnOption_CreerUnEcole_CreerUnFaculterAvecLEcole_CreerUnFilierePourLaFaculte_CreerUnOptionPourLaFiliere_CreerDeuxSpecialiesPourLOption_OntenirLesSpecialitesDeLOption_RenvoiBienLesSpecialites()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");
            _dal.CreerFaculte("Science", ecole);
            var faculte = _dal.ObtenirFaculte("Science");
            _dal.CreerFiliere("Matematique", faculte);
            var filiere = _dal.ObtenirFiliere("Matematique");
            _dal.CreerOption("Quantique", filiere);
            var option = _dal.ObtenirOption(1);


            _dal.CreerSpecialite("S1", option);
            _dal.CreerSpecialite("S2", option);

            var specialitesDlOption = _dal.ObtenirListeSpecialitesDeLOption(option);

            option = _dal.ObtenirOption(1);
            Assert.IsNotNull(option.Specialites);
            Assert.AreEqual(2, option.Specialites.Count);
            Assert.IsNotNull(specialitesDlOption);

            Assert.AreEqual(2, specialitesDlOption.Count);
            Assert.AreEqual("S1", specialitesDlOption[0].Nom);
            Assert.AreEqual("S2", specialitesDlOption[1].Nom);

        }

        #endregion


        #region Specialite

        [TestMethod]
        public void CreerSpecialite_AvecUnNouveEcole_EtUnNouveuFaculte_EtUnNouveauFiliere_EtUnNouveauOption_EtUneNouvelleSpecialite_ObtientTousLesSpecialitesRenvoitBienLESpecialite()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");
            _dal.CreerFaculte("Science", ecole);
            var faculte = _dal.ObtenirFaculte("Sience");
            _dal.CreerFiliere("Mathematique", faculte);
            var filiere = _dal.ObtenirFiliere("Mathematique");
            _dal.CreerOption("Mecanique", filiere);
            var option = _dal.ObtenirOption(1);
            _dal.CreerSpecialite("Quantique", option);

            var spacialites = _dal.ObtenirListeSpecialites();
            Assert.IsNotNull(spacialites);
            Assert.AreEqual(1, spacialites.Count);
            Assert.AreEqual("Quantique", spacialites[0].Nom);
        }

        [TestMethod]
        public void ModificerSupprimerSpacialite_AvecDeuxNouvauxSpacialites_ObtientTousLesSpacialite_ModifierLePremier_SupprimerLeSecond_RenvoitBienLaModificationEtNullSurLeSupprimer()
        {
            _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            var ecole = _dal.ObtenirEcole("IFT");
            _dal.CreerFaculte("Scien", ecole);
            var faculte = _dal.ObtenirFaculte(1);
            _dal.CreerFiliere("Mathematique", faculte);
            var filiere = _dal.ObtenirFiliere("Mathematique");
            _dal.CreerOption("Mecanique", filiere);
            var option = _dal.ObtenirOption(1);

            _dal.CreerSpecialite("S1", option);
            _dal.CreerSpecialite("S2", option);
            var specialites = _dal.ObtenirListeSpecialites();
            var premier = _dal.ObtenirSpecialite(specialites[0].Id);
            var second = _dal.ObtenirSpecialite(specialites[1].Nom);
            Assert.IsNotNull(specialites);
            Assert.AreEqual(2, specialites.Count);
            Assert.AreEqual("S1", premier.Nom);
            Assert.AreEqual("S2", second.Nom);
            _dal.ModifierSpecialite(premier.Id, "Quantique");
            _dal.SupprimerSpecialite(second.Id);
            Assert.AreEqual("Quantique", premier.Nom);
            second = _dal.ObtenirSpecialite(2);
            Assert.IsNull(second);
        }

        #endregion

        #region Membre 

        [TestMethod]
        public void
            CreerDeuxMembre_AvecUnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_ObtientLeMembreLaListeDesMembre()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            Assert.IsNotNull(ecole);
            var faculte = _dal.CreerFaculte("Science", ecole);
            Assert.IsNotNull(faculte);
            var filiere = _dal.CreerFiliere("Mathematique", faculte);
            Assert.IsNotNull(filiere);
            var option = _dal.CreerOption("Mathématiques appliquée", filiere);
            Assert.IsNotNull(option);
            var specialite = _dal.CreerSpecialite("Mécanique", option);
            Assert.IsNotNull(specialite);
             _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
            var membre = _dal.ObtenirMembre(1);
            var membre2 = _dal.CreerMembre("Randre2", "Zo2", "Zo002", "II2300Tazo2", "test@ts2.com", Privilege.Professeur, "hreyrey2",
                specialite);


            Assert.IsNotNull(membre);
            Assert.AreEqual("Randre", membre.Nom);
            Assert.AreEqual("Zo", membre.Prenom);
            Assert.AreEqual("Zo00", membre.Pseudo);
            Assert.AreEqual("II2300Tazo", membre.Adresse);
            Assert.AreEqual("test@ts.com", membre.Email);
            Assert.AreEqual(Privilege.Etudiant, membre.Privilege);
            Assert.AreEqual(Tools.Outils.EncodeMd5("hreyrey"), membre.MotDePasse);
            Assert.IsNotNull(membre.Specialite);
            Assert.AreEqual("Mécanique", membre.Specialite.Nom);

            var lstmembres = _dal.ObtenirListeMembres();

            Assert.IsNotNull(lstmembres);
            Assert.AreEqual(2, lstmembres.Count);
            Assert.AreEqual("Randre2", lstmembres[1].Nom);
            Assert.AreEqual("Zo2", lstmembres[1].Prenom);
            Assert.AreEqual("Zo002", lstmembres[1].Pseudo);
            Assert.AreEqual("II2300Tazo2", lstmembres[1].Adresse);
            Assert.AreEqual("test@ts2.com", lstmembres[1].Email);
            Assert.AreEqual(Privilege.Professeur, lstmembres[1].Privilege);
            Assert.AreEqual(Tools.Outils.EncodeMd5("hreyrey2"), lstmembres[1].MotDePasse);
            Assert.IsNotNull(lstmembres[1].Specialite);
            Assert.AreEqual("Mécanique", lstmembres[1].Specialite.Nom);


        }


        [TestMethod]
        public void
           RechercheMembre_AvecUnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_RechercherLesMembres()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            Assert.IsNotNull(ecole);
            var faculte = _dal.CreerFaculte("Science", ecole);
            Assert.IsNotNull(faculte);
            var filiere = _dal.CreerFiliere("Mathematique", faculte);
            Assert.IsNotNull(filiere);
            var option = _dal.CreerOption("Mathématiques appliquée", filiere);
            Assert.IsNotNull(option);
            var specialite = _dal.CreerSpecialite("Mécanique", option);
            Assert.IsNotNull(specialite);
            _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
               specialite);
            var membre = _dal.RechercheMembres("Zo")[0];
             _dal.CreerMembre("Be2", "Zo2", "Zo002", "II2300Tazo2", "test@ts2.com", Privilege.Professeur, "hreyrey2",
                specialite);
            var membre2 = _dal.RechercheMembres("Be2")[0];

            Assert.IsNotNull(membre);
            Assert.AreEqual("Randre", membre.Nom);
            Assert.AreEqual("Zo", membre.Prenom);
            Assert.AreEqual("Zo00", membre.Pseudo);
            Assert.AreEqual("II2300Tazo", membre.Adresse);
            Assert.AreEqual("test@ts.com", membre.Email);
            Assert.AreEqual(Privilege.Etudiant, membre.Privilege);
            Assert.AreEqual(Tools.Outils.EncodeMd5("hreyrey"), membre.MotDePasse);
            Assert.IsNotNull(membre.Specialite);
            Assert.AreEqual("Mécanique", membre.Specialite.Nom);





        }


        [TestMethod]
        public void
            VerifierSiPseudoEtEmailExiste_AvecCreerDeuxMembre_UnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_RetourneTrueSiPseudoEtEmailExisteDejaEtFalseSiPseudoEtEmailNExistePas()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            Assert.IsNotNull(ecole);
            var faculte = _dal.CreerFaculte("Science", ecole);
            Assert.IsNotNull(faculte);
            var filiere = _dal.CreerFiliere("Mathematique", faculte);
            Assert.IsNotNull(filiere);
            var option = _dal.CreerOption("Mathématiques appliquée", filiere);
            Assert.IsNotNull(option);
            var specialite = _dal.CreerSpecialite("Mécanique", option);
            Assert.IsNotNull(specialite);
            var membre = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
            var membre2 = _dal.CreerMembre("Randre2", "Zo2", "Zo002", "II2300Tazo2", "test@ts2.com", Privilege.Professeur, "hreyrey2",
                specialite);

            Assert.IsTrue( _dal.PseudoMembreExisteDeja(membre.Pseudo));
            Assert.IsTrue(_dal.PseudoMembreExisteDeja(membre2.Pseudo));
            Assert.IsFalse(_dal.PseudoMembreExisteDeja("iea"));
            Assert.IsTrue(_dal.EmailMembreExisteDeja(membre.Email));
            Assert.IsTrue(_dal.EmailMembreExisteDeja(membre2.Email));
            Assert.IsFalse(_dal.EmailMembreExisteDeja("iea"));


        }

        [TestMethod]
        public void
           AuThentificationMembreCreerUnMembre_AvecUnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_AuthentifierLemembre()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");
            Assert.IsNotNull(ecole);
            var faculte = _dal.CreerFaculte("Science", ecole);
            Assert.IsNotNull(faculte);
            var filiere = _dal.CreerFiliere("Mathematique", faculte);
            Assert.IsNotNull(filiere);
            var option = _dal.CreerOption("Mathématiques appliquée", filiere);
            Assert.IsNotNull(option);
            var specialite = _dal.CreerSpecialite("Mécanique", option);
            Assert.IsNotNull(specialite);
            var membre = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
            membre = _dal.Authentifier("Zo00", "hreyrey");


            Assert.IsNotNull(membre);
            Assert.AreEqual("Randre", membre.Nom);
            Assert.AreEqual("Zo", membre.Prenom);
            Assert.AreEqual("Zo00", membre.Pseudo);
            Assert.AreEqual("II2300Tazo", membre.Adresse);
            Assert.AreEqual("test@ts.com", membre.Email);
            Assert.AreEqual(Privilege.Etudiant, membre.Privilege);
            Assert.AreEqual(Tools.Outils.EncodeMd5("hreyrey"), membre.MotDePasse);

           
        }

        [TestMethod]
        public void
            ModifocationEtSuppressionMembre_AvecDeuxMembres_CreerUnTopicEtUnDocument_SupprimerLePremier_ModifierLeSecond_ObtientLeMembreSupplimerEgaleNullEtObtenirMembreModitie()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);

            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
            var membre2 = _dal.CreerMembre("HH", "Zrrt", "grer", "455", "GgGGGEER@ts.com", Privilege.Professeur, "trvg",
                specialite);

            //
            _dal.CreerTopic("titre teste", "0123456789", membre1, "theme teste", DateTime.Now, "description01");
            _dal.CreerDocument("titre teste", "doc1", "/de/de", membre1, "theme1", DateTime.Now, "description01", 10);
            _dal.SupprimerMembre(membre1.Id);


            _dal.ModifierMembre(membre2.Id, "Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Administrateur,
               "hreyrey",
                specialite);
            membre1 = _dal.ObtenirMembre(membre1.Id);
            Assert.IsNull(membre1);
            membre2 = _dal.ObtenirMembre(membre2.Id);
            Assert.IsNotNull(membre2);
            Assert.AreEqual("Randre", membre2.Nom);
            Assert.AreEqual("Zo", membre2.Prenom);
            Assert.AreEqual("Zo00", membre2.Pseudo);
            Assert.AreEqual("II2300Tazo", membre2.Adresse);
            Assert.AreEqual("test@ts.com", membre2.Email);
            Assert.AreEqual(Privilege.Administrateur, membre2.Privilege);
            Assert.AreEqual(Tools.Outils.EncodeMd5("hreyrey"), membre2.MotDePasse);
        }

        [TestMethod]
        public void
            AttribuerDesMembreAUnSpecialite_CreerUnEcole_CreerUnFaculterAvecLEcole_CreerUnFilierePourLaFaculte_CreerUnOptionPourLaFiliere_CreerUnSpecialiesPourLOption_CreerDeuxmembrePourLESpecialite_RenvoiBienLesMembreDuSpecialite_Option()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);

            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
            var membre2 = _dal.CreerMembre("HH", "Zo2", "grer", "455", "GgGGGEER@ts.com", Privilege.Professeur, "trvg",
                specialite);


            var lstMembres = _dal.ObtenirListeMembreDuSpecialite(specialite);
            Assert.IsNotNull(lstMembres);
            Assert.AreEqual(2, lstMembres.Count);
            Assert.AreEqual("Randre", lstMembres[0].Nom);
            Assert.AreEqual("Zo2", lstMembres[1].Prenom);

        }



        #endregion

        #region topic

        [TestMethod]
        public void
            CreerDeuxTopics_AvecUnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_ObtientLeTopicEtLalisteDesTopics()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);
            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
            var datemantenant = DateTime.Now;
            var topic1 = _dal.CreerTopic("titre teste", "0123456789", membre1, "theme teste", datemantenant,"description01");
            var topic2 = _dal.CreerTopic("titre teste2", "01234567892", membre1, "theme teste2", datemantenant, "description02");
            var lstTopics = _dal.ObtenirListeTopics();

            membre1 = _dal.ObtenirMembre(membre1.Id);
            Assert.IsNotNull(membre1.Topics);
            Assert.IsNotNull(topic1);

            Assert.IsNotNull(lstTopics);
            Assert.AreEqual(2, lstTopics.Count);
            Assert.AreEqual("titre teste", lstTopics[0].Titre);
            Assert.AreEqual("titre teste2", lstTopics[1].Titre);
            Assert.AreEqual("0123456789", lstTopics[0].Contenu);
            Assert.AreEqual("01234567892", lstTopics[1].Contenu);
            Assert.AreEqual("Randre", lstTopics[0].Auteur.Nom);
            Assert.AreEqual("Randre", lstTopics[1].Auteur.Nom);
            Assert.AreEqual("theme teste", lstTopics[0].Theme);
            Assert.AreEqual("theme teste2", lstTopics[1].Theme); 
            Assert.AreEqual("description01", lstTopics[0].Description);
            Assert.AreEqual("description02", lstTopics[1].Description);
        }


        [TestMethod]
        public void ObtenirTopicsParOrderDePublication_AvecUnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_CreerDeuxTopic_ObtenirLesDeuxTopicParOrdreDePublication()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);
            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
          
            var topic1 = _dal.CreerTopic("premier", "0123456789", membre1, "theme teste", DateTime.Now, "description01");
            var topic2 = _dal.CreerTopic("second", "01234567892", membre1, "theme teste2", DateTime.Now, "description02");
            var lstTopics = _dal.ObtenirListeDerniersTopics(0);


            Assert.IsNotNull(lstTopics);
            Assert.AreEqual(2, lstTopics.Count);
            Assert.AreEqual("second", lstTopics[0].Titre);
            Assert.AreEqual("premier", lstTopics[1].Titre);
           
        }

        [TestMethod]
        public void RechercheTopics_AvecUnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_CreerDeuxTopics_ObtenirLesTopicsParRecherche()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);
            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);

             _dal.CreerTopic("premier", "0123456789", membre1, "theme teste", DateTime.Now, "description01");
             _dal.CreerTopic("second", "01234567892", membre1, "theme teste2", DateTime.Now, "description02");

            var topic1 = _dal.RechercheTopics("premier")[0];
            var topic2 = _dal.RechercheTopics("second")[0];

            Assert.AreEqual("premier", topic1.Titre);
            Assert.AreEqual("second", topic2.Titre);

        }

        [TestMethod]
        public void
            ModificationEtSuppressionTopic_AvecDeuxTopics_SupprimerLePremier_ModifierLeSecond_ObtientLeTopicSupplimEEgaleNullEtObtenirTopicModifie()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);

            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);

            var datemantenant = DateTime.Now;
            var topic1 = _dal.CreerTopic("titre teste", "0123456789", membre1, "theme teste", datemantenant, "description01");
            var topic2 = _dal.CreerTopic("titre teste2", "01234567892", membre1, "theme teste2", datemantenant, "description02");



            _dal.SupprimerTopic(topic1.Id);
            _dal.ModifierTopic(topic2.Id, "titre teste", "0123456789", "theme teste", "descriptionModif");

            topic1 = _dal.ObtenirTopic(topic1.Id);
            Assert.IsNull(topic1);
            topic2 = _dal.ObtenirTopic(topic2.Id);
            Assert.IsNotNull(topic2);
            Assert.AreEqual("titre teste", topic2.Titre);
            Assert.AreEqual("0123456789", topic2.Contenu);
            Assert.AreEqual("Randre", topic2.Auteur.Nom);
            Assert.AreEqual("theme teste", topic2.Theme); 
            Assert.AreEqual("descriptionModif", topic2.Description);
        }
        #endregion

        #region document

        [TestMethod]
        public void
            CreerDeuxDocuments_AvecUnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_ObtientLeTopicEtLalisteDesdocuments()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);
            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
            var datemantenant = DateTime.Now;
            var cheminDoc1 = membre1.Specialite.Option.Filere.Faculte.Ecole.Nom + "/" + membre1.Specialite.Option.Filere.Faculte.Nom + "/" + DateTime.Now.Date.ToString();
            var document1 = _dal.CreerDocument("titre teste", "doc1", cheminDoc1, membre1, "theme1", datemantenant, "description01",10);
            var document2 = _dal.CreerDocument("titre teste2", "doc2", cheminDoc1, membre1, "theme2", datemantenant, "description02",11);
            var lstDocuments = _dal.ObtenirListeDocuments();

            Assert.IsNotNull(document1);

            Assert.IsNotNull(lstDocuments);
            Assert.AreEqual(2, lstDocuments.Count);
            Assert.AreEqual("titre teste", lstDocuments[0].Titre);
            Assert.AreEqual("titre teste2", lstDocuments[1].Titre);
            Assert.AreEqual("doc1", lstDocuments[0].Nom);
            Assert.AreEqual("doc2", lstDocuments[1].Nom);
            Assert.AreEqual("Randre", lstDocuments[0].Auteur.Nom);
            Assert.AreEqual("Randre", lstDocuments[1].Auteur.Nom);
            Assert.AreEqual("theme1", lstDocuments[0].Theme);
            Assert.AreEqual("theme2", lstDocuments[1].Theme);
            Assert.AreEqual("description01", lstDocuments[0].Description);
            Assert.AreEqual("description02", lstDocuments[1].Description);
            Assert.AreEqual(10, lstDocuments[0].NbPages);
            Assert.AreEqual(11, lstDocuments[1].NbPages);
        }


        [TestMethod]
        public void
            RechercheDocuments_AvecUnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_ObtientLesDeuxDocumentsParRecherche()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);
            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
            var datemantenant = DateTime.Now;
            var cheminDoc1 = membre1.Specialite.Option.Filere.Faculte.Ecole.Nom + "/" + membre1.Specialite.Option.Filere.Faculte.Nom + "/" + DateTime.Now.Date.ToString();
             _dal.CreerDocument("titre teste", "doc1", cheminDoc1, membre1, "theme1", datemantenant, "description01", 10);
             _dal.CreerDocument("titre teste2", "doc2", cheminDoc1, membre1, "theme2", datemantenant, "description02", 11);
          
            var document1 = _dal.RechercheDocuments("doc1")[0];
            var document2 = _dal.RechercheDocuments("doc2")[0];



            Assert.AreEqual("doc1", document1.Nom);
            Assert.AreEqual("doc2", document2.Nom);

        }

        [TestMethod]
        public void ObtenirDocumentsParOrderDePublication_AvecUnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_CreerDeuxDocuments_ObtenirLesDeuxDocumentsParOrdreDePublication()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);
            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);

            var doc1 = _dal.CreerDocument("premier", "0123456789","1", membre1, "theme teste", DateTime.Now, "description01",10);
            var doc2 = _dal.CreerDocument("second", "01234567892","2", membre1, "theme teste2", DateTime.Now, "description02",11);
            var lstDocuments = _dal.ObtenirListeDerniersDocuments(0);


            Assert.IsNotNull(lstDocuments);
            Assert.AreEqual(2, lstDocuments.Count);
            Assert.AreEqual("second", lstDocuments[0].Titre);
            Assert.AreEqual("premier", lstDocuments[1].Titre);

        }

     

        [TestMethod]
        public void
            ModificationEtSuppressionDocument_AvecDeuxDocuments_SupprimerLePremier_ModifierLeSecond_ObtientLeDocumentSupplimEEgaleNullEtObtenirDocumenteModifie()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);

            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);

            var datemantenant = DateTime.Now;
            var document1 = _dal.CreerDocument("titre teste", "0123456789","1", membre1, "theme teste", datemantenant, "description01",10);
            var document2 = _dal.CreerDocument("titre teste2", "01234567892","2", membre1, "theme teste2", datemantenant, "description02",11);



            _dal.SupprimerDocument(document1.Id);
            _dal.ModifierDocument(document2.Id, "titre teste", "0123456789", "theme teste", "descriptionModif");

            document1 = _dal.ObtenirDocument(document1.Id);
            Assert.IsNull(document1);
            document2 = _dal.ObtenirDocument(document2.Id);
            Assert.IsNotNull(document2);
            Assert.AreEqual("titre teste", document2.Titre);
            Assert.AreEqual("0123456789", document2.Nom);
            Assert.AreEqual("Randre", document2.Auteur.Nom);
            Assert.AreEqual("theme teste", document2.Theme);
            Assert.AreEqual("descriptionModif", document2.Description);

        }
        #endregion

        #region Commentaire
        [TestMethod]
        public void
            CreerDeuxCommentaire_AvecUnNouvelEcole_NouveuDepartement_NouveauFaculte_NouveauFiliere_NouveauOption_NouvauxSpacialites_NouvauxTopique_NouveauDocument_CreerDeuxCommentaireUnPourTopicUnPourDocument_ObtientListeDesCommentairesEtLeCommentairePourLeTopicEtLaDocument()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);
            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
            var datemantenant = DateTime.Now;
            var topic1 = _dal.CreerTopic("titre topic", "contenu", membre1, "theme1", datemantenant, "description01");
            var document1 = _dal.CreerDocument("titre document", "/jj/101","1", membre1, "theme2", datemantenant, "description02",11);
            var lstDocuments = _dal.ObtenirListeDocuments();


            var commnetaire1 = _dal.CreerCommentaire(topic1, membre1, "test comment 1", datemantenant);
            var commnetaire2 = _dal.CreerCommentaire(document1, membre1, "test comment 2", datemantenant);

            var lstCommentaires = _dal.ObtenirListeCommentaires();
            //Assert.IsNotNull(document1);

            

            Assert.IsNotNull(lstCommentaires);
            Assert.AreEqual(2, lstCommentaires.Count);
            Assert.AreEqual("test comment 1", lstCommentaires[0].Contenu);
            Assert.AreEqual("test comment 2", lstCommentaires[1].Contenu);
            Assert.AreEqual("Randre", lstCommentaires[0].Auteur.Nom);
            Assert.AreEqual("Randre", lstCommentaires[1].Auteur.Nom);
            Assert.AreEqual("titre topic", lstCommentaires[0].Publication.Titre);
            Assert.AreEqual("titre document", lstCommentaires[1].Publication.Titre); 
         

            var lstCommentairesTopic1 = topic1.Commentaires;
            var lstCommentairesDocument1 = document1.Commentaires;
            Assert.IsNotNull(lstCommentairesTopic1 );
            Assert.IsNotNull(lstCommentairesDocument1);
            Assert.AreEqual(1, lstCommentairesTopic1.Count);
            Assert.AreEqual(1, lstCommentairesDocument1.Count);
            Assert.AreEqual("test comment 1", lstCommentairesTopic1[0].Contenu);
            Assert.AreEqual("test comment 2", lstCommentairesDocument1[0].Contenu);
        }


        [TestMethod]
        public void
            ModificationEtSuppressionCommentaire_AvecDeuxCommentaire_SupprimerLePremier_ModifierLeSecond_ObtientLeCommentaireSupplimEEgaleNullEtObtenirCommentaireModitie()
        {
            var ecole = _dal.CreerEcole("IFT", "LOT 2I39A Ampandrana", "0330257032", "ift@gmail.com");

            var faculte = _dal.CreerFaculte("Science", ecole);

            var filiere = _dal.CreerFiliere("Mathematique", faculte);

            var option = _dal.CreerOption("Mathématiques appliquée", filiere);

            var specialite = _dal.CreerSpecialite("Mécanique", option);
            var membre1 = _dal.CreerMembre("Randre", "Zo", "Zo00", "II2300Tazo", "test@ts.com", Privilege.Etudiant, "hreyrey",
                specialite);
            var datemantenant = DateTime.Now;
            var topic1 = _dal.CreerTopic("titre topic", "contenu", membre1, "theme1", datemantenant, "description01");
            var document1 = _dal.CreerDocument("titre document", "/jj/101","1", membre1, "theme2", datemantenant, "description02",11);
            var lstDocuments = _dal.ObtenirListeDocuments();


            var commnetaire1 = _dal.CreerCommentaire(topic1, membre1, "test comment 1", datemantenant);
            var commnetaire2 = _dal.CreerCommentaire(document1, membre1, "test comment 2", datemantenant);

            _dal.ModifierCommentaire(commnetaire2.Id, "modifComm");
            _dal.SupprimerCommentaire(commnetaire1.Id);
            commnetaire1 = _dal.ObtenirCommentaire(commnetaire1.Id);
            commnetaire2 = _dal.ObtenirCommentaire(commnetaire2.Id);

            Assert.IsNull(commnetaire1);
            Assert.AreEqual("modifComm", commnetaire2.Contenu);
          
        }


        #endregion
    }
}
