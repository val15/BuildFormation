using BuildFormation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildFormation.ViewModels
{
    public class RechercheTopicViewModel
    {
        public string Filtre { get; set; }
        public List<Topic> ListeDesTopics { get; set; }
    }
}