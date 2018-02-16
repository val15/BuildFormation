using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public class Membre
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        [Required]
        public string Pseudo { get; set; }
        [Required]
        public string Adresse { get; set; }

        [Required]
        [RegularExpression(@"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "L'adresse mail est incorrecte")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }

        public Privilege Privilege { get; set; }
        public  Specialite Specialite { get; set; }

        public virtual List<Topic> Topics { get; set; }

        public virtual List<Document> Documents { get; set; }


    }

    
}