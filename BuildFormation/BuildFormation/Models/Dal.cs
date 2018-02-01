using System;
using System.Collections.Generic;
using System.Linq;
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
        public void CreerEcole(string nom, string adresse, string telephone, string email)
        {
            _bdd.Ecoles.Add(new Ecole {Nom = nom, Adresse = adresse, Telephone = telephone, Email = email});
            try
            {
                _bdd.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
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

        public List<Faculte> ObtenirListeFacultesDUnEcole(int idEcole)
        {
            return _bdd.Facultes.Where(f => f.Ecole.Id == idEcole).ToList();
        }

        public List<Faculte> ObtenirListeFacultesDUnEcole(Ecole ecole)
        {
            return _bdd.Facultes.Where(f => f.Ecole.Id == ecole.Id).ToList();
        }





        #endregion


        #region Faculte

        public void CreerFaculte(string nom,Ecole ecole)
        {
            _bdd.Facultes.Add(new Faculte{ Nom = nom , Ecole = ecole});
            try
            {
                _bdd.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

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
                return _bdd.Filieres.Where(f => f.Faculte.Id == faculte.Id).ToList();
        }

        #endregion


        #region Filiere

        public void CreerFiliere(string nom, Faculte faculte)
        {
            _bdd.Filieres.Add(new Filiere { Nom = nom, Faculte = faculte });
            try
            {
                _bdd.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

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

        public List<Option> ObtenirListeOptionesDUnFaculte(Filiere filiere)
        {
            return _bdd.Options.Where(o => o.Filere.Id == filiere.Id).ToList();
        }


        #endregion


        #region Option

        public void CreerOption(string nom, Filiere filiere)
        {
            _bdd.Options.Add(new Option { Nom = nom, Filere = filiere });
            try
            {
                _bdd.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

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
            return _bdd.Specialites.Where(s => s.Option.Id == option.Id).ToList();

        }

        #endregion

        #region Specialite

        public void CreerSpecialite(string nom, Option option)
        {
            _bdd.Specialites.Add(new Specialite { Nom = nom,  Option = option });
            try
            {
                _bdd.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

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

        #endregion



        public void Dispose()
        {
            _bdd.Dispose();
        }
    }
}