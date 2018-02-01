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
            _dal.CreerFaculte("Science", ecole);

            var faculte = _dal.ObtenirFaculte("Science");
            _dal.CreerFiliere("F1",faculte);
            _dal.CreerFiliere("F2", faculte);

            var filieresDuFaculte= _dal.ObtenirListeFileresDUnFaculte(faculte);
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
            _dal.CreerFiliere("Mathematique",faculte);
           

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
            _dal.CreerFiliere("F1",faculte);
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

            var optionsDuFiliere = _dal.ObtenirListeOptionesDUnFaculte(filiere);
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

            _dal.CreerOption("Mecanique",filiere);
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
            _dal.CreerSpecialite("Quantique",option);

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




    }
}
