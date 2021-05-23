using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hackathon
{
    /// <summary>
    /// Logique d'interaction pour ProfilJoueur.xaml
    /// </summary>
    public partial class ProfilJoueur : Window
    {
        public ProfilJoueur(string nom, string prenom, string pseudo, string nationalite, string jeux, string type_Joueur, string points, string mail, string image)
        {
            InitializeComponent();
            lab_nom.Text = nom;
            lab_prenom.Text = prenom;
            lab_pseudo.Text = pseudo;
            lab_jeu.Text = jeux;
            lab_nationalite.Text = nationalite;
            lab_mail.Text = mail;
            lab_points.Text = points;
            img.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + image, UriKind.Absolute));
            if (type_Joueur == "True")
            {
                lab_statut.Text = "Pro";
            }
            else
            {
                lab_statut.Text = "Amateur";
            }
        }
    }
}
