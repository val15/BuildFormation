using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BuildFormation.Models;

namespace BuildFormation.ViewModels
{
    public class ModifierCompteViewModel
    {
        public Membre Membre { get; set; }
        [Display(Name = "Nouveau mot de passe : ")]
        public string NouveauMotDePasse1 { get; set; }
        [Display(Name = "Retaper le nouveau mot de passe : ")]
        public string NouveauMotDePasse2 { get; set; }
    }
}