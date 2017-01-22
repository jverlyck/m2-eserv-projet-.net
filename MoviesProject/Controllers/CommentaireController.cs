using MoviesProject.Models;
using MoviesProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesProject.Controllers
{
    public class CommentaireController : Controller
    {
        private IDal dal;

        public CommentaireController() : this(new Dal()) 
        {
        }

        public CommentaireController(IDal dal)
        {
            this.dal = dal;
        }

        public ActionResult CreerCommentaire(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Utilisateur utilisateur = dal.RecupererUtilisateur(HttpContext.User.Identity.Name);
                if (utilisateur.Role == "ADMIN" || utilisateur.Role == "USER")
                {
                    ViewBag.nomFilm = dal.RecupererFilm(id.Value).Nom;
                    return View();
                }
            }

            return RedirectToAction("Index", "Film");
        }

        [HttpPost]
        public ActionResult CreerCommentaire(int? id, Commentaire commentaire)
        {
            if (!ModelState.IsValid)
                return View(commentaire);

            Utilisateur utilisateur = dal.RecupererUtilisateur(HttpContext.User.Identity.Name);
            dal.AjouterCommentaire(commentaire.Contenu, utilisateur.Id, id.Value);

            return RedirectToAction("Index", "Film");
        }
    }
}