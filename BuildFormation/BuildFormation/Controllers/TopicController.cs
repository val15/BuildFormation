using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildFormation.Models;
using BuildFormation.ViewModels;

namespace BuildFormation.Controllers
{
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
                return View(topic);
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

    }
}