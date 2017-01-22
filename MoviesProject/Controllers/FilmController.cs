using MoviesProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesProject.Controllers
{
    public class FilmController : Controller
    {
        private IDal dal;

        public FilmController() : this(new Dal()) 
        {
        }

        public FilmController(IDal dal)
        {
            this.dal = dal;
        }

        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                IDal dal = new Dal();
                Utilisateur utilisateur = dal.RecupererUtilisateur(HttpContext.User.Identity.Name);
                ViewBag.role = utilisateur.Role;
            }

            List<Film> listeDesFilms = dal.RecupererTousLesFilms();
            return View(listeDesFilms);
        }

        public ActionResult CreerFilm()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Utilisateur utilisateur = dal.RecupererUtilisateur(HttpContext.User.Identity.Name);
                if (utilisateur.Role == "ADMIN")
                    return View();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreerFilm(Film film)
        {
            if (dal.FilmExisteDeja(film.Nom))
            {
                ModelState.AddModelError("Nom", "Ce film existe déjà");
                return View(film);
            }
            if (!ModelState.IsValid)
                return View(film);

            dal.CreerFilm(film.Nom, film.Genre, film.DateDeSortie, film.Description);

            return RedirectToAction("Index");
        }

        public ActionResult ModifierFilm(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Utilisateur utilisateur = dal.RecupererUtilisateur(HttpContext.User.Identity.Name);
                if (utilisateur.Role == "ADMIN")
                {
                    if (id.HasValue)
                    {
                        Film film = dal.RecupererTousLesFilms().FirstOrDefault(r => r.Id == id.Value);
                        if (film == null)
                            return View("Error");

                        return View(film);
                    }
                }       
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult ModifierFilm(Film film)
        {
            if (!ModelState.IsValid)
                return View(film);
            dal.ModifierFilm(film.Id, film.Nom, film.Genre, film.DateDeSortie, film.Description);

            return RedirectToAction("Index");
        }

        public ActionResult SupprimerFilm(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Utilisateur utilisateur = dal.RecupererUtilisateur(HttpContext.User.Identity.Name);
                if (utilisateur.Role == "ADMIN")
                {
                    if (id.HasValue)
                    {
                        dal.SupprimerFilm(id.Value);
                    }
                }    
            }
            
            return RedirectToAction("Index");
        }
    }
}