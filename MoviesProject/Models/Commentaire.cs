using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesProject.Models
{
    public class Commentaire
    {
        public int Id { get; set; }
        [Required]
        public string Contenu { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
        public virtual Film Film { get; set; }
    }
}