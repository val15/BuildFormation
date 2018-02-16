using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildFormation.Models;
using BuildFormation.ViewModels;

namespace BuildFormation.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly IDal _dal;

        public DocumentController() : this(new Dal())
        {
        }
        public DocumentController(IDal dalIoc)
        {
            _dal = dalIoc;
        }
        public ActionResult Index()
        {

            var listeDocuments = _dal.ObtenirListeDerniersDocuments(0);
            return View(listeDocuments);
        }

        public ActionResult AfficherDocument(int? id)
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

        public string Consulter(int? id)
        {
            if (id.HasValue)
            {
                var document = _dal.ObtenirDocument(id);
                if (document == null)
                    return "Error";
                return "<a href = ../../Documents/"+document.Nom+">telecharger</a>";
            }
            else
                return "NotFound";
        }


        public ActionResult AfficherDocumentsDe(int? id)
        {
            if (id.HasValue)
            {
                var membre = _dal.ObtenirMembre(id);
                var listeDocumentsDuMembre = membre.Documents.OrderByDescending(t => t.DateDePublication).ToList();
                {
                    var documentsDeViewModel = new DocumentsDeViewModel
                    {
                        ListeDocumentsDuMembre = listeDocumentsDuMembre,
                        Membre = membre
                    };
                    return View(documentsDeViewModel);
                }
            }
            else
                return HttpNotFound();
        }
    }
}