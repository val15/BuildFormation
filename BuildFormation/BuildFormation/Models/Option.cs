using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public class Option
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }

        public  Filiere Filere { get; set; }

        public virtual List<Specialite> Specialites { get; set; }

    }
}