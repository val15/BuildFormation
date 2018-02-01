﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public class Faculte
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public virtual Ecole Ecole { get; set; }

        //prof responsable
    }
}