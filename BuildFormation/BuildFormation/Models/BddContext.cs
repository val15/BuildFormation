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

    }
}