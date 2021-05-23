using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logique d'interaction pour AjoutJoueur.xaml
    /// </summary>
    public partial class AjoutJoueur : Window
    {
        //Données joueurs
        private string nom;
        private string prenom;
        private string pseudo;
        private string nationalite;
        private string jeu;
        private bool pro;
        private int points;
        private string mail;
        //Données objet
        private string filePath;
        private bool controleSaisie = false;
        List<string> Jeux = new List<string>();
        List<string> lines;
        private string imgPathAbsolute;
        private string imgPathFinal;
        private string extension;
        //Flags controle de saisie
        private bool flagNom;
        private bool flagPrenom;
        private bool flagPseudo;
        private bool flagNatio;
        private bool flagJeu;
        private bool flagPoints;
        private bool flagMail;
        private bool flagImage;
        public AjoutJoueur(string lien , List<string> lines)
        {
            InitializeComponent();
            filePath = lien;
            this.lines = lines;
            Jeux.Add("Rocket League");
            Jeux.Add("League Of Legends");
            Jeux.Add("CSGO");
            Jeux.Add("Valorant");

            cbJeu.ItemsSource = Jeux;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog photoProfil = new OpenFileDialog();
            photoProfil.Multiselect = false;
            photoProfil.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            photoProfil.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (photoProfil.ShowDialog() == true)
            {
                flagImage = true;
                photoPrevisu.Source = new BitmapImage(new Uri(photoProfil.FileName));
                imgPathAbsolute = photoProfil.FileName;
                extension = System.IO.Path.GetExtension(imgPathAbsolute);
            }
            else
            {
                flagImage = false;
            }
        }

        private void Previsu(object sender, TextChangedEventArgs e)
        {
            bool pseudo_existe = false;

            if (tbNom.Text != "")
            {
                lbNom.Content = tbNom.Text;
                nom = lbNom.Content.ToString();
                flagNom = true;
            }
            else
            {
                flagNom = false;
            }
            if (tbPrenom.Text != "")
            {
                lbPrenom.Content = tbPrenom.Text;
                prenom = lbPrenom.Content.ToString();
                flagPrenom = true;
            }
            else
            {
                flagPrenom = false;
            }

            if (File.ReadAllText(filePath) == "")
            {
                lbPseudo.Content = tbPseudo.Text;
                pseudo = lbPseudo.Content.ToString();
            }

            foreach (string line in lines)
            {
                string[] entries = line.Split('#');
                if (tbPseudo.Text == entries[2])
                {
                    pseudo_existe = true;
                }
                lbPseudo.Content = tbPseudo.Text;
                pseudo = lbPseudo.Content.ToString();
            }

            if(pseudo_existe == true)
            {
                infoPseudo.Text = "Ce pseudo est déjà pris";
                flagPseudo = false;
            }
            if(pseudo_existe == false && tbPseudo.Text != "")
            {
                flagPseudo = true;
                infoPseudo.Text = "";
            }
            
            if (tbPoints.Text != "")
            {
                int outputvalue;
                lbPoints.Content = tbPoints.Text;
                if(int.TryParse(tbPoints.Text, out outputvalue) == false)
                {
                    infosPoints.Text = "Veuillez entrer seulement des chiffres";
                    tbPoints.Text = "";
                    lbPoints.Content = "";
                    flagPoints = false;
                }
                else
                {
                    infosPoints.Text = "";
                    points = Convert.ToInt32(tbPoints.Text);
                    flagPoints = true;
                }
            }
            else
            {
                flagPoints = false;
            }
            if (tbNatio.Text != "")
            {
                lbNatio.Content = tbNatio.Text;
                nationalite = lbNatio.Content.ToString();
                flagNatio = true;
            }
            else
            {
                flagNatio = false;
            }
            if (tbMail.Text != "")
            {
                lbMail.Content = tbMail.Text;
                mail = lbMail.Content.ToString();
                flagMail = true;
            }
            else
            {
                flagMail = false;
            }
        }

        private void StatutPro(object sender, RoutedEventArgs e)
        {
            if (checkPro.IsChecked == true)
            {
                lbStatut.Content = "Pro";
                pro = true;
            }
            else
            {
                lbStatut.Content = "Amateur";
                pro = false;
            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            if (flagJeu == true && flagMail == true && flagNatio == true && flagNom == true && flagPoints == true && flagPrenom == true && flagPseudo == true && flagImage == true)
            {
                controleSaisie = true;
            }
            else
            {
                controleSaisie = false;
            }
            if (controleSaisie == true)
            {
                imgPathFinal = AppDomain.CurrentDomain.BaseDirectory + @"..\..\pictures\" + pseudo + extension;
                File.Copy(imgPathAbsolute, imgPathFinal);
                string combined = nom + "#" + prenom + "#" + pseudo + "#" + nationalite + "#" + jeu + "#" + pro + "#" + points + "#" + mail + "#" + @"..\..\pictures\" + pseudo + extension;
                List<string> lignes = File.ReadAllLines(filePath).ToList();
                lignes.Add(combined);
                File.WriteAllLines(filePath, lignes);
                this.Close();
            }
            else
            {
                string flagged = null;
                if (flagNom == false)
                {
                    flagged += ", Nom";
                }
                if (flagPrenom == false)
                {
                    flagged += ", Prenom";
                }
                if (flagPseudo == false)
                {
                    flagged += ", Pseudo";
                }
                if (flagJeu == false)
                {
                    flagged += ", Jeu";
                }
                if (flagNatio == false)
                {
                    flagged += ", Nationalité";
                }
                if (flagPoints == false)
                {
                    flagged += ", Points";
                }
                if (flagMail == false)
                {
                    flagged += ", Mail";
                }
                if (flagImage == false)
                {
                    flagged += ", Image";
                }
                MessageBox.Show("Vous avez fait une erreur dans la saisie ! Veuillez renseignez les champs suivant" + flagged + ".", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void cbSelection(object sender, SelectionChangedEventArgs e)
        {
            if (cbJeu.SelectedItem != null)
            {
                lbJeu.Content = cbJeu.SelectedItem;
                jeu = lbJeu.Content.ToString();
                flagJeu = true;
            }
            else
            {
                flagJeu = false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow first = new MainWindow();
            first.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Return == e.Key) //Si Touche "ENTER" du clavier => Fais la recherche
            {
                Valider_Click(sender, e);
            }
        }
    }
}
