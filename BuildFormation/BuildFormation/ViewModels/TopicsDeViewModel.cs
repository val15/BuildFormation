using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildFormation.ViewModels
{
    public class TopicsDeViewModel
    {
        public List<Models.Topic> ListeTopicsDuMembre { get; set; }
        public Models.Membre Membre { get; set; }
    }
}