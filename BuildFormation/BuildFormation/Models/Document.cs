using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public class Document : Publication
    {
        [Required]
        public string Nom { get; set; }

        [Required]
        public string Chemin { get; set; }
        [Required]
        public int NbPages { get; set; }
        public virtual Membre Auteur { get; set; }
    }
}