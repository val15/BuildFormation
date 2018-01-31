using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public class Faculte
    {
        public int Id { get; set; }
        [Required]//pour dire que Prenom est obligatoir et une taille max de 80
        public string Nom { get; set; }

        public virtual Ecole Ecole { get; set; }

        //prof responsable
    }
}