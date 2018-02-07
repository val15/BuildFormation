using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildFormation.Models;

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

        public ActionResult AfficherMembre(int? id)
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

    }
}