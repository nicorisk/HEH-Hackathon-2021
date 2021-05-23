using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon
{
    class Joueur
    {
        string nom, prenom, pseudo, nationalite, jeu, typeJoueur,points , mail;

        public Joueur(string nom, string prenom, string pseudo, string nationalite, string jeu, string pro , string points, string mail)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.pseudo = pseudo;
            this.nationalite = nationalite;
            this.jeu = jeu;
            this.mail = mail;
            this.points = points;
            if(pro == "True")
            {
                typeJoueur = "Pro";
            }
            else
            {
                typeJoueur = "Amateur";
            }
        }

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Pseudo { get => pseudo; set => pseudo = value; }
        public string Nationalite { get => nationalite; set => nationalite = value; }
        public string Jeu { get => jeu; set => jeu = value; }
        public string TypeJoueur { get => typeJoueur; set => typeJoueur = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Points { get => points; set => points = value; }
    }
}
