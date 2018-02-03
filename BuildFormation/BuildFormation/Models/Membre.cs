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
        public string Prenom { get; set; }
        [Required]
        public string Pseudo { get; set; }
        [Required]
        public string Adresse { get; set; }
      
        [RegularExpression(@"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "L'adresse mail est incorrecte")]
        public string Email { get; set; }

        [Required]
        public Privilege Privilege { get; set; }

        [Required]
        public string MotDePasse { get; set; }

        


        public  Specialite Specialite { get; set; }


    }

    
}