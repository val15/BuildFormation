using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildFormation.ViewModels
{
    public class DocumentsDeViewModel
    {
        public List<Models.Document> ListeDocumentsDuMembre { get; set; }
        public Models.Membre Membre { get; set; }

    }
}