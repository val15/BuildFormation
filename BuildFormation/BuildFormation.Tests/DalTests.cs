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

            _dal.CreerFaculte("Science",ecole);
            _dal.CreerFaculte("Droit",ecole);
            ecole = _dal.ObtenirEcole("IFT");
            
             var facultesDeLEcole = _dal.ObtenirListeFacultesDUnEcole(ecole);
            Assert.IsNotNull(facultesDeLEcole);
            
            Assert.AreEqual(2, facultesDeLEcole.Count);
            Assert.AreEqual("Science", facultesDeLEcole[0].Nom);
            Assert.AreEqual("Droit", facultesDeLEcole[1].Nom);
        }


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

            _dal.CreerFaculte("Scien",ecole);
            _dal.CreerFaculte("Test",ecole);
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
            Assert.IsNull( second);
        }
    }
}
