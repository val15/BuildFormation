using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BuildFormation.Models
{
    public class InitDB : DropCreateDatabaseAlways<BddContext>
    {
        protected override void Seed(BddContext context)//cette classe saire à initialise notre base
        {

            context.Ecoles.Add(new Ecole { Nom = "IFT",Adresse = "LOT 2I39A Ampandrana", Telephone = "0330257032", Email = "ift@gmail.com"});
            context.SaveChanges();
            var ecole = context.Ecoles.ToList().Last();
            context.Facultes.Add(new Faculte {Nom = "Science", Ecole = ecole});
            context.SaveChanges();
            var faculte = context.Facultes.ToList().Last();
            context.Filieres.Add(new Filiere {Nom = "Mathematique", Faculte = faculte});
            context.SaveChanges();
            var filiere = context.Filieres.ToList().Last();
            context.Options.Add(new Option {Nom = "Mathématiques appliquée", Filere = filiere});
            context.SaveChanges();
            var option = context.Options.ToList().Last();
            context.Specialites.Add(new Specialite {Nom = "Mécanique", Option = option});
            context.SaveChanges();
            var specialite = context.Specialites.ToList().Last();

          
            context.Membres.Add(new Membre
            {
                Nom = "Randre",
                Prenom = "Zo",
                Pseudo = "z",
                Adresse = "19BisTana",
                Email = "test@ts.com",
                Privilege = Privilege.Etudiant,
                MotDePasse = EncodeMd5("z"),//IL FAU SHACHER LE MOT DE PASSE
                Specialite = specialite
               
            });
            context.SaveChanges();
            var membre1 = context.Membres.ToList().Last();
            context.Membres.Add(new Membre
            {
                Nom = "LePony",
                Prenom = "Fiaritso",
                Pseudo = "e",
                Adresse = "19BisTana",
                Email = "test0jhhre@ts.com",
                Privilege = Privilege.Professeur,
                MotDePasse = EncodeMd5("e"),
                Specialite = specialite

            });
            context.SaveChanges();
            var membre2 = context.Membres.ToList().Last();


            context.Topics.Add(new Topic
            {
                Titre = "topic 00",
                Auteur = membre1,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce totic est un simple teste",
                Contenu = "Je m’amuse depuis peu avec Windows 10 IOT et un Raspberry Pi alors je me suis procuré un écran tactile pour améliorer mon" +
                          " expérience. Voici mon setup actuel:"+
                        "Raspberry Pi 3"+
                        "Rapsberry Pi 7” touch display"+
                        "Raspberry Pi Clear Case"+
                        "Windows 10 IOT Insider Edition "+
                        "Pour faire fonctionner le tout,l faut simplement connecter le câble “display” "+
                        "(le gros gris) ainsi que les 3 petits fils sur le GPIO du Raspberry Pi!",
                    });

            context.Topics.Add(new Topic
            {
                Titre = "topic 01",
                Contenu = "simple test 2",
                Auteur = membre1,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce totic est second teste"
            });

            context.Topics.Add(new Topic
            {
                Titre = "topic 03",
                Contenu = "simple test 2",
                Auteur = membre1,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce totic est second teste"
            });



            context.Topics.Add(new Topic
            {
                Titre = "topic 04",
                Contenu = "simple test 2",
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce totic est second teste"
            });

            context.Topics.Add(new Topic
            {
                Titre = "topic 05",
                Contenu = "simple test 2",
                Auteur = membre1,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce totic est second teste"
            });


            context.Topics.Add(new Topic
            {
                Titre = "topic 06",
                Contenu = "\\[ \\int_{a}^{b} f(x) \\, \\mathrm{d}x \\]",
                Auteur = membre1,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce totic est second teste"
            });

            context.Topics.Add(new Topic
            {
                Titre = "topic 07",
                Contenu = "simple test 2",
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce totic est second teste"
            });

            context.Topics.Add(new Topic
            {
                Titre = "topic 09",
                Contenu = "simple test 2",
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce totic est second teste"
            });

            context.Topics.Add(new Topic
            {
                Titre = "topic 11",
                Contenu = "simple test 2",
                Auteur = membre1,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce totic est second teste"
            });

            context.Topics.Add(new Topic
            {
                Titre = "topic 12",
                Contenu = "\\[ \\int_{a}^{b} f(x) \\, \\mathrm{d}x \\]",
                Auteur = membre1,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce totic est second teste"
            });

            context.Documents.Add(new Document
            {
                Titre = "document 00",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 01",
                Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 02",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 03",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 04",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 05",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 06",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 07",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 08",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 09",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 10",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });


            context.Documents.Add(new Document
            {
                Titre = "document 11",
                 Nom = "DocTest.pdf",
                NbPages = 14,
                Auteur = membre2,
                Theme = "theme teste",
                DateDePublication = DateTime.Now,
                Description = "ce docoment est second teste"
            });








            context.SaveChanges();
            //   var topic1 = _dal.CreerTopicTopic(");
            //  var topic2 = _dal.CreerTopicTopic("titre teste2", "01234567892", membre1, "theme teste2", DateTime.Now);
            // var lstTopics = _dal.ObtenirListeTopics();

            base.Seed(context);
        }
        public string EncodeMd5(string motDePasse)
        {
            string motDePasseSel = "BuildFormation" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}