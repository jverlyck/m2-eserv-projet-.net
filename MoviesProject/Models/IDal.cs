using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesProject.Models
{
    public interface IDal : IDisposable
    {
        // Table Film
        void CreerFilm(string nom, string genre, DateTime dateDeSortie, string description);
        void ModifierFilm(int id, string nom, string genre, DateTime dateDeSortie, string description);
        void SupprimerFilm(int id);
        Film RecupererFilm(int id);
        Film RecupererFilm(string nomFilm);
        List<Film> RecupererTousLesFilms();
        bool FilmExisteDeja(string nom);

        // Table Utilisateur
        Utilisateur Authentifier(string nom, string motDePasse);
        Utilisateur RecupererUtilisateur(int id);
        Utilisateur RecupererUtilisateur(string idStr);
        int AjouterUtilisateur(String nom, string motDePasse, string role);

        // Table Commentaire
        void AjouterCommentaire(string contenu, int idUtilisateur, int idFilm);
        List<Commentaire> RecupererTousLesCommentairesDuFilm(int idFilm);
    }
}
