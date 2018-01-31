using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public class Ecole
    {
        public int Id { get; set; }
        [Required]//pour dire que Prenom est obligatoir et une taille max de 80
        public string Nom { get; set; }
        public string Adresse { get; set; }
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le numéro de téléphone est incorrect")]
        public string Telephone { get; set; }

        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "L'adresse mail est incorrecte")]
        public string Email { get; set; }

        //Liste des facultés
        public virtual List<Faculte> Facultes { get; set; }
    }
}