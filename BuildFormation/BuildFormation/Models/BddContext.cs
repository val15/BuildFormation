using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BuildFormation.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Ecole> Ecoles { get; set; }
        public DbSet<Faculte> Facultes { get; set; }
        public DbSet<Filiere> Filieres { get; set; }

        public DbSet<Option> Options { get; set; }
        public DbSet<Specialite> Specialites { get; set; }
        public DbSet<Membre> Membres { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
    }
}