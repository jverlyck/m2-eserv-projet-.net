using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoviesProject.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>()
                .HasMany(f => f.Commentaires)
                .WithRequired(f => f.Film)
                .WillCascadeOnDelete();
        }
    }
}