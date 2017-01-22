using MoviesProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesProject.ViewModels
{
    public class UtilisateurViewModel
    {
        public Utilisateur Utilisateur { get; set; }
        public bool Authentifie { get; set; }
    }
}