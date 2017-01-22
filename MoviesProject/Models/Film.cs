using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesProject.Models
{
    public class Film
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public string Genre { get; set; }
        [Display(Name = "Date de sortie")]
        public DateTime DateDeSortie { get; set; }
        public string Description { get; set; }
        public virtual List<Commentaire> Commentaires { get; set; }
    }
}