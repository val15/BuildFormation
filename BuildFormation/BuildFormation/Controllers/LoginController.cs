using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
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
            List<SelectListItem> ecoleItems = new List<SelectListItem>();
            var listeEcoles = _dal.ObtenirListeEcoles();
            foreach (Ecole ecole in listeEcoles)
            {
                ecoleItems.Add(new SelectListItem { Text = ecole.Nom, Value = ecole.Id.ToString() });

            }
            
            List<SelectListItem> faculteItems = new List<SelectListItem>();
            List<SelectListItem> filiereItems = new List<SelectListItem>();
            List<SelectListItem> optionItems = new List<SelectListItem>();
            List<SelectListItem> specialiteItems = new List<SelectListItem>();

            ViewBag.Ecole = ecoleItems;
            ViewBag.Faculte = faculteItems;
            ViewBag.Filiere = filiereItems;
            ViewBag.Option = optionItems;
            ViewBag.Specialite = specialiteItems;
            CreerCompteViewModel creerCompteViewModel = new CreerCompteViewModel();
            return View(creerCompteViewModel);
        }

        //pour le traitement des json
        #region Json
        public JsonResult ChargerLesFacultesDeLEcole(string nomEcole)
        {
            Ecole ecole = null;
            List<Faculte> listeFacultes = null;
            List<SelectListItem> faculteItems = new List<SelectListItem>();

            if (nomEcole == "Choisissez votre ecole")
            {
                return Json(faculteItems, JsonRequestBehavior.AllowGet);

            }
            else
            {
                ecole = _dal.ObtenirEcole(nomEcole);
                listeFacultes = _dal.ObtenirListeFacultesDUnEcole(ecole);

            }
             foreach (Faculte faculte in listeFacultes)
            {
                faculteItems.Add(new SelectListItem { Text = faculte.Nom, Value = faculte.Id.ToString() });
            }
            return Json(faculteItems, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChargerLesFilieresDeLaFaculte(string nomFaculte)
        {
            Faculte faculte = null;
            List<Filiere> listeFilieres = null;
            List<SelectListItem> filiereItems = new List<SelectListItem>();

            if (nomFaculte == "Choisissez votre faculte")
            {
                return Json(filiereItems, JsonRequestBehavior.AllowGet);

            }
            else
            {
                faculte = _dal.ObtenirFaculte(nomFaculte);
                listeFilieres = _dal.ObtenirListeFileresDUnFaculte(faculte);

            }
               foreach (Filiere filiere in listeFilieres)
            {
                filiereItems.Add(new SelectListItem { Text = filiere.Nom, Value = filiere.Id.ToString() });
            }
            return Json(filiereItems, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChargerLesOptionsDuFiliere(string nomFiliere)
        {
            Filiere filiere = null;
            List<Option> listeOptions = null;
            List<SelectListItem> optionItems = new List<SelectListItem>();

            if (nomFiliere == "Choisissez votre filiere")
            {
                return Json(optionItems, JsonRequestBehavior.AllowGet);
            }
            else
            {
                filiere = _dal.ObtenirFiliere(nomFiliere);
                listeOptions = _dal.ObtenirListeOptionesDUnFiliere(filiere);

            }
            foreach (Option option in listeOptions)
            {
                optionItems.Add(new SelectListItem { Text = option.Nom, Value = option.Id.ToString() });
            }
            return Json(optionItems, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChargerSpecialitesDeLOption(string nomOption)
        {
            Option option = null;
            List<Specialite>  listeSpecilites = null;
            List<SelectListItem> specialiteItems = new List<SelectListItem>();

            if (nomOption== "Choisissez votre option")
            {
                return Json(specialiteItems, JsonRequestBehavior.AllowGet);
            }
            else
            {
                option = _dal.ObtenirOption(nomOption);
                listeSpecilites = _dal.ObtenirListeSpecialitesDeLOption(option);

            }
              foreach (Specialite specialite in listeSpecilites)
            {
                specialiteItems.Add(new SelectListItem { Text = specialite.Nom, Value = specialite.Id.ToString() });
            }
            return Json(specialiteItems, JsonRequestBehavior.AllowGet);
        }

       

        #endregion

        [HttpPost]
        public ActionResult CreerCompte(string Specialite, CreerCompteViewModel creerCompteViewModel)
        {
            
              var specialite = _dal.ObtenirSpecialite(int.Parse(Specialite));

                if (_dal.PseudoMembreExisteDeja(creerCompteViewModel.Pseudo) || _dal.EmailMembreExisteDeja(creerCompteViewModel.Email) || creerCompteViewModel.MotDePasse != creerCompteViewModel.MotDePasseDeConfiramtion || specialite == null)
                {
                    if(_dal.PseudoMembreExisteDeja(creerCompteViewModel.Pseudo))
                        ModelState.AddModelError("Membre.Pseudo", "Ce pseudo de restaurant existe déjà");
                    if(_dal.EmailMembreExisteDeja(creerCompteViewModel.Email))
                        ModelState.AddModelError("Email", "Cet adresse emai est déja pris");
                    if(creerCompteViewModel.MotDePasse!=creerCompteViewModel.MotDePasseDeConfiramtion)
                    {
                        ModelState.AddModelError("MotDePasse", "Les deux mots de passe ne sont pas identiques");
                        ModelState.AddModelError("MotDePasseDeConfiramtion", "Les deux mots de passe ne sont pas identiques");
                    }
                    if (specialite == null)
                        ModelState.AddModelError("Specialite", "Vous devez choisir une spécialitée");


                    List <SelectListItem> ecoleItems = new List<SelectListItem>();
                    var listeEcoles = _dal.ObtenirListeEcoles();
                    foreach (Ecole ecole in listeEcoles)
                    {
                        ecoleItems.Add(new SelectListItem { Text = ecole.Nom, Value = ecole.Id.ToString() });

                    }

                    List<SelectListItem> faculteItems = new List<SelectListItem>();
                    List<SelectListItem> filiereItems = new List<SelectListItem>();
                    List<SelectListItem> optionItems = new List<SelectListItem>();
                    List<SelectListItem> specialiteItems = new List<SelectListItem>();

                    ViewBag.Ecole = ecoleItems;
                    ViewBag.Faculte = faculteItems;
                    ViewBag.Filiere = filiereItems;
                    ViewBag.Option = optionItems;
                    ViewBag.Specialite = specialiteItems;
                   return View(creerCompteViewModel);


                }
                
                var nouveauMembre = _dal.CreerMembre(creerCompteViewModel.Nom, creerCompteViewModel.Prenom, creerCompteViewModel.Pseudo, creerCompteViewModel.Adresse, creerCompteViewModel.Email, Privilege.Etudiant, creerCompteViewModel.MotDePasse, specialite)  ;//pour le privilege et le Specialite on met
               // specialiteCreerCompte = null;
                FormsAuthentication.SetAuthCookie(nouveauMembre.Id.ToString(), false);

                return Redirect("/");
            

        }
        [HttpPost]
        public JsonResult VerifierPseudoMembre(string Pseudo)
        {
            bool resultat = !_dal.PseudoMembreExisteDeja(Pseudo);
            return Json(resultat);
        }

        [HttpPost]
        public JsonResult VerifierEmailMembre(string Email)
        {
            bool resultat = !_dal.EmailMembreExisteDeja(Email);
            return Json(resultat);
        }

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

       



      
    }
}