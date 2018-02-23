using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildFormation.Models;
using BuildFormation.ViewModels;
using Spire.Pdf;

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
                    return HttpNotFound();
                var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);

                AfficherDocumentViewModel afficherDocumentViewModel = new AfficherDocumentViewModel{ MembreActuel = membre, Document = document };
                return View(afficherDocumentViewModel);
            }
            else
                return HttpNotFound();
        }

        public ActionResult Consulter(int? id)
        {
            if (id.HasValue)
            {
                var document = _dal.ObtenirDocument(id);
                if (document == null)
                    return HttpNotFound();
                return this.File(document.Chemin + "/" + document.Nom, "application/pdf", document.Nom);
            }
            else
                return HttpNotFound();
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

        public ActionResult CreerDocument()
        {
            var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);
            var document = new Document();

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Action", Value = "0" });

            items.Add(new SelectListItem { Text = "Drama", Value = "1" });

            items.Add(new SelectListItem { Text = "Comedy", Value = "2", Selected = true });

            items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" });

            ViewBag.MovieType = items;


            return View(document);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file,Document document)
        {
            if (document.Titre==null || document.Theme == null || document.Description == null)
            {
                return View("CreerDocument");
            }
            //     document.Chemin = @"~\Documents\" + "test";
            document.Chemin = "~/Documents/" + "departementtest/"+"themetest/"+DateTime.Now.ToString("yyyy/MM/dd");
            //  Directory.CreateDirectory(document.Chemin);
            var folder = Server.MapPath(document.Chemin);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            try
            {
                
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    var extension = _FileName.Split('.')[_FileName.Split('.').Length - 1];
                    if (extension != "pdf")
                    {
                        ViewBag.Message = "le finchier choisi n'est pas au format requis";
                        return View("CreerDocument");
                    }
                    string _path = Path.Combine(Server.MapPath(document.Chemin), _FileName);
                    file.SaveAs(_path);
                    document.Nom = _FileName;
                    document.NbPages = NbPageDuDocument(document.Chemin + "/" + document.Nom);
                }
                var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);
                
                _dal.CreerDocument(document.Titre, document.Nom,document.Chemin,membre, document.Theme, DateTime.Now, document.Description,document.NbPages);
                ViewBag.Message = "Fichier bien envoyer!";

                return RedirectToAction("AfficherDocumentsDe", new { id = membre.Id });

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

        
         public ActionResult ModifierDocument(int? id)
        {
            if (id.HasValue)
            {
                var document = _dal.ObtenirDocument(id);
                if (document == null)
                    return HttpNotFound();
                return View(document);
            }
            else
                return HttpNotFound();
        }


        [HttpPost]
        public ActionResult EditUploadFile(HttpPostedFileBase file, Document document)
        {
            if (document.Titre == null || document.Theme == null || document.Description == null)
            {
                return View("ModifierDocument");
            }
            //     document.Chemin = @"~\Documents\" + "test";
            document.Chemin = "~/Documents/" + "departementtest/" + "themetest/" + DateTime.Now.ToString("yyyy/MM/dd");
            //  Directory.CreateDirectory(document.Chemin);
            var folder = Server.MapPath(document.Chemin);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            try
            {

                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    var extension = _FileName.Split('.')[_FileName.Split('.').Length - 1];
                    if (extension != "pdf")
                    {
                        ViewBag.Message = "le finchier choisi n'est pas au format requis";
                        return View("ModifierDocument");
                    }
                    string _path = Path.Combine(Server.MapPath(document.Chemin), _FileName);
                    file.SaveAs(_path);
                    document.Nom = _FileName;
                    document.NbPages = NbPageDuDocument(document.Chemin + "/" + document.Nom);
                }
                var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);

                _dal.CreerDocument(document.Titre, document.Nom, document.Chemin, membre, document.Theme, DateTime.Now, document.Description, document.NbPages);
                ViewBag.Message = "Fichier bien envoyer!";

                return RedirectToAction("AfficherDocumentsDe", new { id = membre.Id });

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }


      


            public int NbPageDuDocument(string cheminEtNomDuFichier)
        {
            PdfDocument document = new PdfDocument();


            string FileName = cheminEtNomDuFichier;

            // "~/Documents/departementtest/themetest/2018/02/21/unlock.pdf"
            var cheminReel = Server.MapPath(FileName);

            document.LoadFromFile(cheminReel);

            return document.Pages.Count;
        }
    }
}