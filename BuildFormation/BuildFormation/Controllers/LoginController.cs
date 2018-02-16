using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BuildFormation.Models;
using BuildFormation.ViewModels;

namespace BuildFormation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDal _dal;

        public LoginController() : this(new Dal())
        {

        }

        private LoginController(IDal dalIoc)
        {
            _dal = dalIoc;
                   }

        public ActionResult Index()
        {
            MembreViewModel membreViewModel = new MembreViewModel{ Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)//si l'utilisateur est authontifié
            {
                membreViewModel.Membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);//on le recupere le membre
            }
            return View(membreViewModel);
        }

        [HttpPost]
        public ActionResult Index(MembreViewModel membreViewModel, string returnUrl)//Pour traiter le formulaire d'authentification; 
        //returnUrl permetra la redirrection vers la page ou l'utilisateur voulais accéder avant l'authentifiaction
        {
            if (ModelState.IsValid)//validation du model
            {

                // var membre = _dal.ObtenirMembre(1);//_dal.Authentifier(membreViewModel.Pseudo, membreViewModel.MotDePasse);
                var membre = _dal.Authentifier(membreViewModel.PseudoOuEmail, membreViewModel.MotDePasse);

                if (membre != null)
                {
                    FormsAuthentication.SetAuthCookie(membre.Id.ToString(), false);//genere le coocki
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))//Url.IsLocalUrl(verifi si returnUrl existe bien sur le site
                        return Redirect(returnUrl);
                    return Redirect("/");
                }
                
            }
            ModelState.AddModelError("Membre.Pseudo", "Pseudo ou met de passe incrrrecte");//ajout l'erreur de la faute d'autentification
            return View(membreViewModel);
        }
        
        public ActionResult CreerCompte()
        {
         

            return View();
        }

        [HttpPost]
        public ActionResult CreerCompte(Membre membre)
        {
            if (ModelState.IsValid)
            {
                var nouveauMembre = _dal.CreerMembre(membre.Nom,membre.Prenom,membre.Pseudo,membre.Adresse,membre.Email,Privilege.Etudiant,membre.MotDePasse,_dal.ObtenirSpecialite("Mécanique"));//pour le privilege et le Specialite on met
                //par défaut en attandant l'implementation du combobox

                FormsAuthentication.SetAuthCookie(nouveauMembre.Id.ToString(), false);

                return Redirect("/");
            }
            return View(membre);
        }

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}