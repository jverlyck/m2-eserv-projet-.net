using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MoviesProject.Models
{
    public class Dal : IDal
    {
        private BddContext bdd;

        public Dal()
        {
            bdd = new BddContext();
        }

        // Table Film
        public List<Film> RecupererTousLesFilms()
        {
            return bdd.Films.ToList();
        }

        public Film RecupererFilm(int id)
        {
            return bdd.Films.FirstOrDefault(f => f.Id == id);
        }

        public bool FilmExisteDeja(string nom)
        {
            return bdd.Films.Any(resto => string.Compare(resto.Nom, nom, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public void CreerFilm(string nom, string genre, DateTime dateDeSortie, string description)
        {
            bdd.Films.Add(new Film { Nom = nom, Genre = genre, DateDeSortie = dateDeSortie, Description = description });
            bdd.SaveChanges();
        }

        public void ModifierFilm(int id, string nom, string genre, DateTime dateDeSortie, string description)
        {
            Film filmTrouve = bdd.Films.FirstOrDefault(film => film.Id == id);
            if (filmTrouve != null)
            {
                filmTrouve.Nom = nom;
                filmTrouve.Genre = genre;
                filmTrouve.DateDeSortie = dateDeSortie;
                filmTrouve.Description = description;
                bdd.SaveChanges();
            }
        }

        public void SupprimerFilm(int id)
        {
            Film filmTrouve = bdd.Films.FirstOrDefault(film => film.Id == id);
            if (filmTrouve != null)
            {
                bdd.Films.Remove(filmTrouve);
                bdd.SaveChanges();
            }
        }

        // Table Utilisateur
        public Utilisateur Authentifier(string nom, string motDePasse)
        {
            string motDePasseEncode = EncodeMD5(motDePasse);
            return bdd.Utilisateurs.FirstOrDefault(u => u.NomUtilisateur == nom && u.MotDePasse == motDePasseEncode);
        }

        public Utilisateur RecupererUtilisateur(int id)
        {
            return bdd.Utilisateurs.FirstOrDefault(u => u.Id == id);
        }

        public Utilisateur RecupererUtilisateur(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
                return RecupererUtilisateur(id);

            return null;
        }

        public int AjouterUtilisateur(string nom, string motDePasse, string role)
        {
            string motDePasseEncode = EncodeMD5(motDePasse);
            Utilisateur utilisateur = new Utilisateur { NomUtilisateur = nom, MotDePasse = motDePasseEncode, Role = role };
            bdd.Utilisateurs.Add(utilisateur);
            bdd.SaveChanges();
            return utilisateur.Id;
        }

        // Table Commentaire
        public void AjouterCommentaire(string contenu, int idUtilisateur, int idFilm)
        {
            Commentaire commentaire = new Commentaire
            {
                Contenu = contenu,
                Utilisateur = bdd.Utilisateurs.First(u => u.Id == idUtilisateur),
                Film = bdd.Films.First(f => f.Id == idFilm)
            };

            Film film = bdd.Films.First(f => f.Id == idFilm);
            if (film.Commentaires == null)
                film.Commentaires = new List<Commentaire>();
            film.Commentaires.Add(commentaire);
            bdd.SaveChanges();
        }

        public List<Commentaire> RecupererTousLesCommentairesDuFilm(int idFilm)
        {
            Film film = bdd.Films.First(f => f.Id == idFilm);
            if (film != null)
                return film.Commentaires;

            return null;
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}