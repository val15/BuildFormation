using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Contenu { get; set; }
        
        [Required]
        public Membre Auteur { get; set; }

        [Required]
        public string Theme { get; set; }

        [Required]
        public DateTime DateDePublication { get; set; }

        public DateTime? DateDernierModification { get; set; }

        //public int Note { get; set; }????
       
    }
}