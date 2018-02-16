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
    public class TopicController : Controller
    {
        private readonly IDal _dal;

        public TopicController() : this(new Dal())
        {
        }
        public TopicController(IDal dalIoc)
        {
            _dal = dalIoc;
        }
        public ActionResult Index()
        {
            var listeTopics= _dal.ObtenirListeDerniersTopics(0);
            return View(listeTopics);
            
        }

        public ActionResult AfficherTopic(int? id)
        {
            if (id.HasValue)
            {
                Topic topic = _dal.ObtenirTopic(id);
                if (topic== null)
                    return View("Error");
                var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);

                AfficherTopicViewModel afficherTopicViewModel = new AfficherTopicViewModel { MembreActuel = membre, Topic = topic };
                return View(afficherTopicViewModel);
            }
            else
                return HttpNotFound();
        }


        public ActionResult AfficherTopicsDe(int? id)
        {
            if (id.HasValue)
            {
                var membre = _dal.ObtenirMembre(id);
                var listeTopicsDuMembre = membre.Topics.OrderByDescending(t => t.DateDePublication).ToList();
                {
                    var topicsDeViewModel = new TopicsDeViewModel
                    {
                        ListeTopicsDuMembre = listeTopicsDuMembre,
                        Membre = membre
                    };
                    return View(topicsDeViewModel);
                }
            }
            else
                return HttpNotFound();
        }

        public ActionResult CreerTopic()
        {
            var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);
            var topic=new Topic{Auteur = membre,Commentaires = new List<Commentaire>(),Contenu = "\\[  \\]",DateDePublication = DateTime.Now,DateDernierModification = null};
            return View(topic);
        }

        [HttpPost]
        public ActionResult CreerTopic(Topic topic)//pour ajouter
        {
            if (topic.Titre=="" || topic.Theme=="" || topic.Description=="")
                return View(topic);
            else
            {
                var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);

                _dal.CreerTopic(topic.Titre, topic.Contenu, topic.Auteur = membre, topic.Theme, DateTime.Now,topic.Description);
                return RedirectToAction("AfficherTopicsDe", new { id = membre.Id });
            }
           
        }

        [HttpPost]
        public ActionResult Appercu(Topic topic)//pour l'appercu
        {
            return View("CreerTopic",topic);
        }


        public ActionResult ModifierTopic(int? id)
        {
            if (id.HasValue)
            {
                var topic = _dal.ObtenirTopic(id);
                if (topic == null)
                    return View("Error");
                return View(topic);
            }
            else
                return HttpNotFound();
        }


        [HttpPost]
        public ActionResult ModifierTopic(Topic topic)//pour modifier
        {
            if (topic.Titre == "" || topic.Theme == "" || topic.Description == "")
                return View(topic);
            else
            {
                
                _dal.ModifierTopic(topic.Id,topic.Titre, topic.Contenu, topic.Theme,topic.Description);
                var membre = _dal.ObtenirMembre(HttpContext.User.Identity.Name);


                return RedirectToAction("AfficherTopicsDe", new { id =membre.Id});
            }

        }

        [HttpPost]
        public ActionResult AppercuModifier(Topic topic)//pour l'appercu
        {
          //  var topicModif = _dal.ObtenirTopic(topic.Id);
            return View("ModifierTopic", topic);
        }






    }
}