using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public abstract class Publication
    {
        public int Id { get; set; }
        [Required]
        public string Titre { get; set; }
      //  [Required]
       // public virtual Membre Auteur { get; set; }
        [Required]
        public string Theme { get; set; }
        [Required]//restriction sur le nombre de caractaire sur le déscription
        public string Description { get; set; }
        [Required]
        public DateTime DateDePublication { get; set; }

        public virtual List<Commentaire> Commentaires { get; set; }

    }
}