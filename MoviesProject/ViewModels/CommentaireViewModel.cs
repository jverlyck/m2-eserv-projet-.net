using MoviesProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesProject.ViewModels
{
    public class CommentaireViewModel
    {
        public Commentaire Commentaire { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public Film Film { get; set; }
    }
}