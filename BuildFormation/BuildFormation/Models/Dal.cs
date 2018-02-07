using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services.Description;

namespace BuildFormation.Models
{
    public class Dal : IDal
    {
        private readonly BddContext _bdd;

        public Dal()
        {
            _bdd = new BddContext();// la création du context entraine la création de la base et de ces table
        }

        #region Ecole
        public Ecole CreerEcole(string nom, string adresse, string telephone, string email)
        {
            _bdd.Ecoles.Add(new Ecole {Nom = nom, Adresse = adresse, Telephone = telephone, Email = email});
            try
            {
                _bdd.SaveChanges();
                return _bdd.Ecoles.ToList().Last();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;

            }
        }

        public bool EcoleExiste(int id)
        {
            var ecole = _bdd.Ecoles.FirstOrDefault(e => e.Id == id);
            return ecole != null;
        }

        public bool ModifierEcole(int id, string nom, string adresse, string telephone, string email)
        {
            var ecole = ObtenirEcole(id);
            if (ecole == null)
                return false;
            else
            {
                try
                {
                    ecole.Nom = nom;
                    ecole.Adresse = adresse;
                    ecole.Telephone = telephone;
                    ecole.Email = email;
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public bool  SupprimerEcole(int id)
        {
            var ecole = ObtenirEcole(id);
            if (ecole == null)
                return false;
            else
            {
                try
                {
                    _bdd.Ecoles.Remove(ecole);
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }

            }
            
        }

        public Ecole ObtenirEcole(int id)
        {
            return _bdd.Ecoles.FirstOrDefault(e => e.Id == id);
        }

        public Ecole ObtenirEcole(string nom)
        {
            return _bdd.Ecoles.FirstOrDefault(e => e.Nom == nom);
        }


        public List<Ecole> ObtenirListeEcoles()
        {
            return _bdd.Ecoles.ToList();
        }

      

        public List<Faculte> ObtenirListeFacultesDUnEcole(Ecole ecole)
        {
           
            return ecole.Facultes.ToList();
        }





        #endregion


        #region Faculte

        public Faculte CreerFaculte(string nom,Ecole ecole)
        {
            try
            {
                _bdd.Facultes.Add(new Faculte{ Nom = nom , Ecole = ecole});
                _bdd.SaveChanges();
                //var lecole = ObtenirEcole(ecole.Id);
                //lecole?.Facultes.Add(_bdd.Facultes.ToList().Last());

            
                
                // _bdd.SaveChanges();
                return _bdd.Facultes.ToList().Last();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;

            }
        }

        public bool ModifierFaculte(int id, string nom)
        {
            var faculte= ObtenirFaculte(id);
            if (faculte == null)
                return false;
            else
            {
                try
                {
                    faculte.Nom = nom;
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

        }

        public bool SupprimerFaculte(int id)
        {
            var faculte = ObtenirFaculte(id);
            if (faculte == null)
                return false;
            else
            {
                try
                {
                    _bdd.Facultes.Remove(faculte);
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public Faculte ObtenirFaculte(int id)
        {
            return _bdd.Facultes.FirstOrDefault(f => f.Id == id);
        }

        public Faculte ObtenirFaculte(string nom)
        {
            return _bdd.Facultes.FirstOrDefault(f => f.Nom == nom);
        }

        public List<Faculte> ObtenirListeFacultes()
        {
            return _bdd.Facultes.ToList();
        }

        public List<Filiere> ObtenirListeFileresDUnFaculte(Faculte faculte)
        {
                //return _bdd.Filieres.Where(f => f.Faculte.Id == faculte.Id).ToList();

            return faculte.Filieres.ToList();
        }

        #endregion


        #region Filiere

        public Filiere CreerFiliere(string nom, Faculte faculte)
        {
           try
            {
                _bdd.Filieres.Add(new Filiere { Nom = nom, Faculte = faculte });
                _bdd.SaveChanges();
                //var laFaculte = ObtenirFaculte(faculte.Id);
                //laFaculte?.Filieres.Add(_bdd.Filieres.ToList().Last());

                //_bdd.SaveChanges();

                return _bdd.Filieres.ToList().Last();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;

            }
        }

        public bool ModifierFiliere(int id, string nom)
        {
            var filiere = ObtenirFiliere(id);
            if (filiere == null)
                return false;
            else
            {
                try
                {
                    filiere.Nom = nom;
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

        }

        public bool SupprimerFiliere(int id)
        {
            var fiere = ObtenirFiliere(id);
            if (fiere == null)
                return false;
            else
            {
                try
                {
                    _bdd.Filieres.Remove(fiere);
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public Filiere ObtenirFiliere(int id)
        {
            return _bdd.Filieres.FirstOrDefault(f => f.Id == id);
        }

        public Filiere ObtenirFiliere(string nom)
        {
            return _bdd.Filieres.FirstOrDefault(f => f.Nom == nom);
        }

        public List<Filiere> ObtenirListeFilieres()
        {
            return _bdd.Filieres.ToList();
        }

        public List<Option> ObtenirListeOptionesDUnFiliere(Filiere filiere)
        {
             return filiere.Options;
        }


        #endregion


        #region Option

        public Option CreerOption(string nom, Filiere filiere)
        {
             
            try
            {
                _bdd.Options.Add(new Option { Nom = nom, Filere = filiere });
                _bdd.SaveChanges();
                //filiere.Options.Add(_bdd.Options.ToList().Last());
                //_bdd.SaveChanges();
                return _bdd.Options.ToList().Last();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool ModifierOption(int id, string nom)
        {
            var option = ObtenirOption(id);
            if (option == null)
                return false;
            else
            {
                try
                {
                    option.Nom = nom;
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

        }

        public bool SupprimerOption(int id)
        {
            var option = ObtenirOption(id);
            if (option == null)
                return false;
            else
            {
                try
                {
                    _bdd.Options.Remove(option);
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public Option ObtenirOption(int id)
        {
            return _bdd.Options.FirstOrDefault(o => o.Id == id);
        }

        public Option ObtenirOption(string nom)
        {
            return _bdd.Options.FirstOrDefault(f => f.Nom == nom);
        }

        public List<Option> ObtenirListeOptions()
        {
            return _bdd.Options.ToList();
        }

        public List<Specialite> ObtenirListeSpecialitesDeLOption(Option option)
        {
           // return _bdd.Specialites.Where(s => s.Option.Id == option.Id).ToList();
            return option.Specialites;
        }

        #endregion

        #region Specialite

        public Specialite CreerSpecialite(string nom, Option option)
        {
            _bdd.Specialites.Add(new Specialite { Nom = nom,  Option = option });
            
            try
            {
                _bdd.SaveChanges();
                //option.Specialites.Add(_bdd.Specialites.ToList().Last());
                //_bdd.SaveChanges();
                return _bdd.Specialites.ToList().Last();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;

            }
        }

        public bool ModifierSpecialite(int id, string nom)
        {
            var specialite = ObtenirSpecialite(id);
            if (specialite == null)
                return false;
            else
            {
                try
                {
                    specialite.Nom = nom;
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

        }

        public bool SupprimerSpecialite(int id)
        {
            var specialite = ObtenirSpecialite(id);
            if (specialite == null)
                return false;
            else
            {
                try
                {
                    _bdd.Specialites.Remove(specialite);
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public Specialite ObtenirSpecialite(int id)
        {
            return _bdd.Specialites.FirstOrDefault(o => o.Id == id);
        }

        public Specialite ObtenirSpecialite(string nom)
        {
            return _bdd.Specialites.FirstOrDefault(f => f.Nom == nom);
        }

        public List<Specialite> ObtenirListeSpecialites()
        {
            return _bdd.Specialites.ToList();
        }

        public List<Membre> ObtenirListeMembreDuSpecialite(Specialite specialite)
        {
            return specialite.Membres;
        }

        #endregion

        #region Membre

        public Membre CreerMembre(string nom, string prenom, string pseudo, string adresse, string email, Privilege privilege,
            string motDePasse, Specialite specialite)
        {
            var mdpHacher = EncodeMd5(motDePasse);

            _bdd.Membres.Add(new Membre
            {
                Nom = nom,
                Prenom = prenom,
                Pseudo = pseudo,
                Adresse = adresse,
                Email = email,
                Privilege = privilege,
                MotDePasse = mdpHacher,
                Specialite = specialite
            });
            try
            {
                _bdd.SaveChanges();
                return _bdd.Membres.ToList().Last();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;

            }
        }

        public bool ModifierMembre(int id, string nom, string prenom, string pseudo, string adresse, string email, Privilege privilege,
            string motDePasse, Specialite specialite)
        {
            var membre = ObtenirMembre(id);
            if (membre == null)
                return false;
            else
            {
                membre.Nom = nom;
                membre.Prenom = prenom;
                membre.Pseudo = pseudo;
                membre.Adresse = adresse;
                membre.Email = email;
                membre.Privilege = privilege;
                membre.Specialite = specialite;
                membre.MotDePasse = EncodeMd5(motDePasse);
            }

            try
            {
                _bdd.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool SupprimerMembre(int id)
        {
            var membre = ObtenirMembre(id);
            if (membre == null)
                return false;
            else
            {
                  try
                {
                    _bdd.Membres.Remove(membre);
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
                
            }
        }

        public Membre ObtenirMembre(int? id)
        {
            return _bdd.Membres.FirstOrDefault(m => m.Id == id);
        }

        public List<Membre> ObtenirListeMembres()
        {
            return _bdd.Membres.ToList();
        }

        public int ObtenirNombreMembre()
        {
            return _bdd.Membres.Count();
        }

        public Membre Authentifier(string pseudoOuAdresseEmail, string motDePasse)
        {
            string mdpHacher = EncodeMd5(motDePasse);
            return _bdd.Membres.FirstOrDefault(m =>
                (m.Pseudo == pseudoOuAdresseEmail || m.Email == pseudoOuAdresseEmail) &&
                m.MotDePasse == mdpHacher);

        }




        public string EncodeMd5(string motDePasse)
        {
            string motDePasseSel = "BuildFormation" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        #endregion

        #region Topic

        public Topic CreerTopic(string titre, string contenu, Membre auteur, string theme, DateTime dateDePublication,string description)
        {
            _bdd.Topics.Add(new Topic
            {
                Titre = titre,
                Contenu = contenu,
                Auteur = auteur,
                Theme = theme,
                DateDePublication = dateDePublication,
                Description = description


            });
            try
            {
                _bdd.SaveChanges();
                return _bdd.Topics.ToList().Last();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool ModifierTopic(int id, string titre, string contenu, string theme, string description)
        {
            var topic = _bdd.Topics.FirstOrDefault(t => t.Id == id);
            if (topic == null)
                return false;
            else
            {
                try
                {
                    topic.Titre = titre;
                    topic.Contenu = contenu;
                    topic.Theme = theme;
                    topic.Description = description;
                    topic.DateDernierModification=DateTime.Now;
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public bool SupprimerTopic(int id)
        {
            var topic = _bdd.Topics.FirstOrDefault(t => t.Id == id);
            if (topic == null)
                return false;
            else
            {
                try
                {
                    _bdd.Topics.Remove(topic);
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public Topic ObtenirTopic(int? id)
        {
            return _bdd.Topics.FirstOrDefault(t => t.Id == id);
        }

        public List<Topic> ObtenirListeTopics()
        {
            //OU  return _bdd.Topics.OrderBy(t => t.DateDePublication).ToList();
            return _bdd.Topics.ToList();

        }

        public List<Topic> ObtenirListeDerniersTopics(int limit)
        {
            return limit==0 ? _bdd.Topics.OrderByDescending(t => t.DateDePublication).ToList() : _bdd.Topics.OrderByDescending(t => t.DateDePublication).Take(limit).ToList();
        }
        #endregion

        #region Document
        public Document CreerDocument(string titre, string chemin, Membre auteur, string theme, DateTime dateDePublication,string description)
        {
            _bdd.Documents.Add(new Document
            {
                Titre = titre,
                Chemin = chemin,
                Auteur = auteur,
                Theme = theme,
                DateDePublication = dateDePublication,
                Description = description
            });
            try
            {
                _bdd.SaveChanges();
                return _bdd.Documents.ToList().Last();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool ModifierDocument(int id, string titre, string chemin, string theme,string description)
        {
            var document = ObtenirDocument(id);
            if (document == null)
                return false;
            else
            {
                try
                {
                    document.Titre = titre;
                    document.Chemin = chemin;
                    document.Theme = theme;
                    document.Description = description;
                   _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public Document ObtenirDocument(int id)
        {
            return _bdd.Documents.FirstOrDefault(d => d.Id==id);
        }

        public List<Document> ObtenirListeDerniersDocuments(int limit)
        {
            return limit == 0 ? _bdd.Documents.OrderByDescending(d => d.DateDePublication).ToList() : _bdd.Documents.OrderByDescending(d => d.DateDePublication).Take(limit).ToList();
        }
        public List<Document> ObtenirListeDocuments()
        {
            return _bdd.Documents.ToList();
        }


        public bool SupprimerDocument(int id)
        {
            var document = ObtenirDocument(id);
            if (document == null)
                return false;
            else
            {
                try
                {
                    _bdd.Documents.Remove(document);
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }


        

        #endregion

        #region Commentaire

        public Commentaire CreerCommentaire(Publication publication, Membre auteur, string contenu, DateTime dateDePublication)
        {
            _bdd.Commentaires.Add(new Commentaire
            {
                Publication = publication,
                Auteur = auteur,
                Contenu = contenu,
                DateDePublication = dateDePublication
            });
            try
            {
                _bdd.SaveChanges();
                return _bdd.Commentaires.ToList().Last();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool ModifierCommentaire(int id, string contenu)
        {
            var commentaire = ObtenirCommentaire(id);
            if (commentaire == null)
                return false;
            else
            {
                try
                {
                    
                     commentaire.Contenu = contenu;
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }

            }
        }

        public Commentaire ObtenirCommentaire(int id)
        {
            return _bdd.Commentaires.FirstOrDefault(c => c.Id == id);

        }

        public List<Commentaire> ObtenirListeCommentaires()
        {
            return _bdd.Commentaires.ToList();
        }

        public bool SupprimerCommentaire(int id)
        {
            var commentaire = ObtenirCommentaire(id);
            if (commentaire == null)
                return false;
            else
            {
                try
                {
                    _bdd.Commentaires.Remove(commentaire);
                    _bdd.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        #endregion



        public void Dispose()
        {
            _bdd.Dispose();
        }
    }
}