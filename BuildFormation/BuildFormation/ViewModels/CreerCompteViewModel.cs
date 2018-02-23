using BuildFormation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildFormation.ViewModels
{
    public class CreerCompteViewModel
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        [Required]
        [Remote("VerifierPseudoMembre", "Login", HttpMethod = "POST", ErrorMessage = "Ce pseudo de restaurant existe déjà")]//pour la verification asynchrone
        public string Pseudo { get; set; }
        [Required]
        public string Adresse { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote("VerifierEmailMembre", "Login", HttpMethod = "POST", ErrorMessage = "Cet adresse emai est déja pris")]//pour la verification asynchrone

        //  [RegularExpression(@"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "L'adresse mail est incorrecte")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }
        [Display(Name = "Retaper le nouveau mot de passe : ")]
        public string MotDePasseDeConfiramtion { get; set; }

        public Specialite Specialite { get; set; }




    }
}