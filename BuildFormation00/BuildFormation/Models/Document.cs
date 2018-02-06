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
        public string Chemin { get; set; }
    }
}