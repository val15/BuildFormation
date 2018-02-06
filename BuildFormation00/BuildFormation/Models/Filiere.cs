using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public class Filiere
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }

        
        public  Faculte Faculte { get; set; }

        public virtual List<Option> Options { get; set; }
    }
}