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
            List<Topic> listeTopics= _dal.ObtenirListeDerniersTopics(0);
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
                var listeTopicsDumembre = membre.Topics;


                if (listeTopicsDumembre == null)
                    return View("Error");
                else
                {
                    var topicsDeViewModel = new TopicsDeViewModel
                    {
                        ListeTopicsDuMembre = listeTopicsDumembre,
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