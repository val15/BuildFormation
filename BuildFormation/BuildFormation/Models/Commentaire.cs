using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public class Commentaire
    {
        public int Id { get; set; }
        public Publication Publication { get; set; }
        public Membre Auteur { get; set; }
        public string Contenu { get; set; }
        public DateTime DateDePublication { get; set; }

     

    }
}