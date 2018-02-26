using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildFormation.Models;
using BuildFormation.ViewModels;

namespace BuildFormation.Controllers
{
    public class MembreController : Controller
    {

        private readonly IDal _dal;

        public MembreController() : this(new Dal())
        {
        }
        public MembreController(IDal dalIoc)
        {
            _dal = dalIoc;
        }

        public ActionResult Index()
        {

            var listeMembre = _dal.ObtenirListeMembres();
            return View(listeMembre);
        }
        //modification compte

        //modification topic
        
        //modidifcation doucument

        //creer topic

        //creer document

        public ActionResult AfficherMembre(int? id)
        {
            if (id.HasValue)
            {
               /* var faculter = _dal.ObtenirFaculte(1);
                var nomFac = faculter.Nom;*/

                var membre = _dal.ObtenirMembre(id);
                if (membre == null)
                    return View("Error");
                var specialite = membre.Specialite;
                var option = specialite.Option;
                var filere = option.Filere;
                var faculte= filere.Faculte;
                var ecole= faculte.Ecole;

                AfficherMembreViewModel afficherMembreViewModel = new AfficherMembreViewModel{ Membre = membre,NomSpecialite=specialite.Nom,NomOption=option.Nom,NomFiliere=filere.Nom,NomFaculte=faculte.Nom,NomEcole=ecole.Nom};

                return View(afficherMembreViewModel);
            }
            else
                return HttpNotFound();
        }

    }
}