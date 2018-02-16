using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BuildFormation.Models;

namespace BuildFormation.ViewModels
{
    public class MembreViewModel
    {
        public Membre Membre { get; set; }
        [Display(Name = "Pseudo ou Email")]
        public string PseudoOuEmail { get; set; }
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }
        public bool Authentifie { get; set; }
    }
}