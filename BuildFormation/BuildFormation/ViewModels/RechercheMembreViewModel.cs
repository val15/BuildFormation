using BuildFormation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildFormation.ViewModels
{
    public class RechercheMembreViewModel
    {
        public string Filtre { get; set; }
        public List<Membre> ListeMembres { get; set; }
    }
}