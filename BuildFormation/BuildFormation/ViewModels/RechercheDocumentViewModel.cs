using BuildFormation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildFormation.ViewModels
{
    public class RechercheDocumentViewModel
    {
        public string Filtre { get; set; }
        public List<Document> ListeDesDocuments { get; set; }
    }
}