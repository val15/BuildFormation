using BuildFormation.Models;
using BuildFormation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildFormation.Controllers
{
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

        }
}