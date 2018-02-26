using BuildFormation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildFormation.ViewModels
{
    public class AfficherMembreViewModel
    {
        public Membre Membre { get; set; }
        public string NomEcole { get; set; }
        public string NomFaculte { get; set; }
        public string NomFiliere { get; set; }
        public string NomOption { get; set; }
        public string NomSpecialite { get; set; }
        
    }
}