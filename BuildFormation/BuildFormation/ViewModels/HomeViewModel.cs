using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildFormation.ViewModels
{
    public class HomeViewModel
    {
        public List<Models.Topic> ListeDerniersTopics { get; set; }
        public List<Models.Document> ListeDerniersDocuments { get; set; }
        public int Limit { get; set; }
        public int NbMembre { get; set; }
    }
}