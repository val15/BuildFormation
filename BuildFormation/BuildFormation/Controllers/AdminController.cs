using BuildFormation.Models;
using BuildFormation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BuildFormation.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IDal _dal;

        public AdminController() : this(new Dal())
        {
        }
        public AdminController(IDal dalIoc)
        {
            _dal = dalIoc;
        }
        public ActionResult Index()
        {
            var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);
            {
                if (membre.Privilege != Privilege.Administrateur)//si le membre n'est pas adrministrateur
                {

                    FormsAuthentication.SignOut();
                    return RedirectToAction("Index", "Login");
                }
            }

           return View();
        }





        #region gestion membre
       
        public ActionResult RechercheMembre()
        {
            return View();
        }
        public ActionResult ResultatsRechercheMembres(RechercheMembreViewModel rechercheMembreViewModel)
        {
            
            if (!string.IsNullOrWhiteSpace(rechercheMembreViewModel.Filtre))
                rechercheMembreViewModel.ListeMembres = _dal.RechercheMembres(rechercheMembreViewModel.Filtre);
            else
                rechercheMembreViewModel.ListeMembres = _dal.ObtenirListeMembres();
            return PartialView(rechercheMembreViewModel);
            
        }

        public ActionResult ModifierPrivilege(int? id)
        {
            if (id.HasValue)
            {
                var membre = _dal.ObtenirMembre(id);
                if (membre == null)
                    return View("Error");
                return View(membre);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult ModifierPrivilege(Membre membre)
        {
            // _dal.ModifierMembre(membre.Id, membre.Nom, membre.Prenom, membre.Pseudo, membre.Adresse, membre.Email, membre.Privilege, membre.MotDePasse, membre.Specialite);
            _dal.ModifierPrivilegeMembre(membre.Id, membre.Privilege);
            return View("Index");
        }

        public ActionResult SupprimerMembre(int? id)
        {
            if (id.HasValue)
            {
                var membre = _dal.ObtenirMembre(id);
                if (membre == null)
                    return View("Error");
                return View(membre);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult SupprimerMembre(Membre membre)
        {
            // _dal.ModifierMembre(membre.Id, membre.Nom, membre.Prenom, membre.Pseudo, membre.Adresse, membre.Email, membre.Privilege, membre.MotDePasse, membre.Specialite);
            _dal.SupprimerMembre(membre.Id);
            return View("Index");
        }
        #endregion

        #region gestion topic
        public ActionResult RechercheTopic()
        {
            return View();
        }
        public ActionResult ResultatsRechercheTopics(RechercheTopicViewModel rechercheTopicViewModel)
        {

            if (!string.IsNullOrWhiteSpace(rechercheTopicViewModel.Filtre))
                rechercheTopicViewModel.ListeDesTopics= _dal.RechercheTopics(rechercheTopicViewModel.Filtre);
            else
                rechercheTopicViewModel.ListeDesTopics = _dal.ObtenirListeTopics();
            return PartialView(rechercheTopicViewModel);

        }

        public ActionResult SupprimerTopic(int? id)
        {
            if (id.HasValue)
            {
                var topic= _dal.ObtenirTopic(id);
                if (topic == null)
                    return View("Error");
                return View(topic);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult SupprimerTopic(Topic topic)
        {
            // _dal.ModifierMembre(membre.Id, membre.Nom, membre.Prenom, membre.Pseudo, membre.Adresse, membre.Email, membre.Privilege, membre.MotDePasse, membre.Specialite);
            _dal.SupprimerTopic(topic.Id);
            return RedirectToAction("RechercheTopic");
        }

        #endregion

        #region gestion des documents
        public ActionResult RechercheDocument()
        {
            return View();
        }

        public ActionResult ResultatsRechercheDocuments(RechercheDocumentViewModel rechercheDocumentViewModel)
        {

            if (!string.IsNullOrWhiteSpace(rechercheDocumentViewModel.Filtre))
                rechercheDocumentViewModel.ListeDesDocuments = _dal.RechercheDocuments(rechercheDocumentViewModel.Filtre);
            else
                rechercheDocumentViewModel.ListeDesDocuments = _dal.ObtenirListeDocuments();
            return PartialView(rechercheDocumentViewModel);

        }

        
         public ActionResult SupprimerDocument(int? id)
        {
            if (id.HasValue)
            {
                var document = _dal.ObtenirDocument(id);
                if (document == null)
                    return View("Error");
                return View(document);
            }
            else
                return HttpNotFound();
        }
        [HttpPost]
        public ActionResult SupprimerDocument(Document document)
        {
            // _dal.ModifierMembre(membre.Id, membre.Nom, membre.Prenom, membre.Pseudo, membre.Adresse, membre.Email, membre.Privilege, membre.MotDePasse, membre.Specialite);
            _dal.SupprimerDocument(document.Id);
          return RedirectToAction("RechercheDocument");
            
        }

        #endregion



    }
}