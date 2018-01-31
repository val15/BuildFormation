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

        #endregion




        public void Dispose()
        {
            _bdd.Dispose();
        }
    }
}