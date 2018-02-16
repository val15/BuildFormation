using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using BuildFormation.Models;
using BuildFormation.ViewModels;

namespace BuildFormation.Controllers
{

    //pour que le membre puisse gérer ses topics et ces publications
    [Authorize]
    public class MomCompteController : Controller
    {
        private readonly IDal _dal;

        public MomCompteController() : this(new Dal())
        {
        }

        public MomCompteController(IDal dalIoc)
        {
            _dal = dalIoc;
        }

        public ActionResult Index()
        {
            //il faut prende le membre actuel

            if (HttpContext.User.Identity.IsAuthenticated) //si l'utilisateur est authontifier
            {


                var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name); //on le recupere


                return View(membre);
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost() //qui redirrige ver la modification du profil
        {
            return RedirectToAction("ModifierCompte", "MomCompte");
        }

        public ActionResult ModifierCompte()
        {
            if (HttpContext.User.Identity.IsAuthenticated) //si l'utilisateur est authontifier
            {

                var modifierCompteViewModel = new ModifierCompteViewModel
                {
                    Membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name) //on le recupere
                };

                return View(modifierCompteViewModel);
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        [HttpPost]
        public ActionResult ModifierCompte(ModifierCompteViewModel momCompteViewModel)
        {

            if (!ModelState.IsValid)
                return View(momCompteViewModel);
            var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);
            if (momCompteViewModel.NouveauMotDePasse1.IsEmpty()) //si le mot de passe ne cahnge pas
            {

                if (Tools.Outils.EncodeMd5(momCompteViewModel.Membre.MotDePasse) != membre.MotDePasse)
                {
                    ModelState.AddModelError("Membre.MotDePasse", "Le mot de passe incorrecte");
                    return View(momCompteViewModel);
                }
                _dal.ModifierMembre(membre.Id, momCompteViewModel.Membre.Nom, momCompteViewModel.Membre.Prenom, momCompteViewModel.Membre.Pseudo, momCompteViewModel.Membre.Adresse, momCompteViewModel.Membre.Email,
                membre.Privilege, membre.MotDePasse, membre.Specialite);
               
            }
            else //si momCompteViewModel.NouveauMotDePasse1 n'est pas vide cela veut dire que l'utilisateur vaut aussi modifier son mdp
            {
                if (Tools.Outils.EncodeMd5(momCompteViewModel.Membre.MotDePasse) != membre.MotDePasse)
                {
                    ModelState.AddModelError("Membre.MotDePasse", "Le mot de passe incorrecte");
                    return View(momCompteViewModel);
                }

                if (momCompteViewModel.NouveauMotDePasse1 != momCompteViewModel.NouveauMotDePasse2)
                {
                    ModelState.AddModelError("Membre.MotDePasse", "Les deux nouveaux mot de passes sont differents");
                  
                    return View(momCompteViewModel);
                }
                _dal.ModifierMembre(membre.Id, momCompteViewModel.Membre.Nom, momCompteViewModel.Membre.Prenom, momCompteViewModel.Membre.Pseudo, momCompteViewModel.Membre.Adresse, momCompteViewModel.Membre.Email,
                    membre.Privilege, momCompteViewModel.NouveauMotDePasse1, membre.Specialite);


            }

            return RedirectToAction("Index");
        }
    }

}