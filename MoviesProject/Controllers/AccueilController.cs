using MoviesProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesProject.Controllers
{
    public class AccueilController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                IDal dal = new Dal();
                Utilisateur utilisateur = dal.RecupererUtilisateur(HttpContext.User.Identity.Name);
                ViewBag.nom = utilisateur.NomUtilisateur;
                ViewBag.role = utilisateur.Role;
            }
            else
            {
                ViewBag.nom = "Anonyme";
                ViewBag.role = "";
            }

            return View();
        }
    }
}