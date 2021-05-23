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
    /// Logique d'interaction pour ModifJoueur.xaml
    /// </summary>
    public partial class ModifJoueur : Window
    {
        //Données joueur initiales
        private string nom;
        private string prenom;
        private string pseudo;
        private string nationalite;
        private string jeu;
        private bool pro;
        private int points;
        private string mail;
        //Données joueur mise à jour
        private string nomMod;
        private string prenomMod;
        private string pseudoMod;
        private string nationaliteMod;
        private string jeuMod;
        private bool proMod;
        private int pointsMod;
        private string mailMod;
        //Données module
        private string filePath ;
        List<string> Jeux = new List<string>();
        List<string> lines;
        private string imgPath;
        private string imgPathAbsoluteMod;
        private string imgPathFinalMod;
        private string extension;
        private string extensionMod;
        private bool flagPseudo;
        private bool pseudo_existe;
        private bool flagImage;
        public ModifJoueur(string filePath , string line , List<string> lines)
        {
            InitializeComponent();
            //Recupération des données du joueur à modifier
            this.filePath = filePath;
            this.lines = lines;
            string[] entries = line.Split('#');
            nom = entries[0];
            prenom = entries[1];
            pseudo = entries[2];
            nationalite = entries[3];
            jeu = entries[4];
            pro = Convert.ToBoolean(entries[5]);
            points = Convert.ToInt32(entries[6]);
            mail = entries[7];
            imgPath = entries[8];
            extension = System.IO.Path.GetExtension(imgPath);
            extensionMod = extension;
            BitmapImage imgprofil = new BitmapImage();
            using (var stream = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + imgPath))
            {
                imgprofil.BeginInit();
                imgprofil.CacheOption = BitmapCacheOption.OnLoad;
                imgprofil.StreamSource = stream;
                imgprofil.EndInit();
            }
            photoPrevisu.Source = imgprofil;
            //Initialisation prévisu
            lbNom.Content = nom;
            lbPrenom.Content = prenom;
            lbPseudo.Content = pseudo;
            lbNatio.Content = nationalite;
            lbJeu.Content = jeu;
            //Initialisation champs de modification (au cas ou on n'edit jamais leurs valeurs)
            nomMod = nom;
            prenomMod = prenom;
            pseudoMod = pseudo;
            nationaliteMod = nationalite;
            jeuMod = jeu;
            proMod = pro;
            pointsMod = points;
            mailMod = mail;
            if (pro == true)
            {
                lbStatut.Content = "Pro";
            }
            else
            {
                lbStatut.Content = "Amateur";
            }
            lbPoints.Content = points;
            lbMail.Content = mail;

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
                BitmapImage imgprofil = new BitmapImage();
                using (var stream = File.OpenRead(photoProfil.FileName))
                {
                    imgprofil.BeginInit();
                    imgprofil.CacheOption = BitmapCacheOption.OnLoad;
                    imgprofil.StreamSource = stream;
                    imgprofil.EndInit();
                }

                photoPrevisu.Source = imgprofil;
                imgPathAbsoluteMod = photoProfil.FileName;
                extensionMod = System.IO.Path.GetExtension(imgPathAbsoluteMod);
                

                flagImage = true;
            }
        }

        private void Previsu(object sender, TextChangedEventArgs e)
        {
            pseudo_existe = false;
            if (tbNom.Text != "")
            {
                lbNom.Content = tbNom.Text;
                nomMod = lbNom.Content.ToString();
            }
            else
            {
                lbNom.Content = nom;
                nomMod = nom;
            }
            if (tbPrenom.Text != "")
            {
                lbPrenom.Content = tbPrenom.Text;
                prenomMod = lbPrenom.Content.ToString();
            }
            else
            {
                lbPrenom.Content = prenom;
                prenomMod = prenom;
            }

            foreach (string line in lines)
            {
                string[] entries = line.Split('#');
                if (tbPseudo.Text == entries[2])
                {
                    pseudo_existe = true;
                    if(tbPseudo.Text == pseudo)
                    {
                        pseudo_existe = false;
                    }
                }
                lbPseudo.Content = tbPseudo.Text;
                pseudoMod = lbPseudo.Content.ToString();
            }
            if (tbPseudo.Text == "")
            {
                lbPseudo.Content = pseudo;
            }

            if (pseudo_existe == true)
            {
                infoPseudo.Text = "Ce pseudo est déjà pris";
                flagPseudo = false;
            }
            if (pseudo_existe == false && tbPseudo.Text != "")
            {
                flagPseudo = true;
                pseudoMod = lbPseudo.Content.ToString();
                infoPseudo.Text = "";
            }
            if (tbPoints.Text != "")
            {
                int outputvalue;
                lbPoints.Content = tbPoints.Text;
                if (int.TryParse(tbPoints.Text, out outputvalue) == false)
                {
                    infosPoints.Text = "Veuillez entrer seulement des chiffres";
                    tbPoints.Text = "";
                    lbPoints.Content = points;
                }
                else
                {
                    infosPoints.Text = "";
                    pointsMod = Convert.ToInt32(tbPoints.Text);
                }
            }
            else
            {
                lbPoints.Content = points;
                pointsMod = points;
            }
            if (tbNatio.Text != "")
            {
                lbNatio.Content = tbNatio.Text;
                nationaliteMod = lbNatio.Content.ToString();
            }
            else
            {
                lbNatio.Content = nationalite;
                nationaliteMod = nationalite;
            }
            if (tbMail.Text != "")
            {
                lbMail.Content = tbMail.Text;
                mailMod = lbMail.Content.ToString();
            }
            else
            {
                lbMail.Content = mail;
                mailMod = mail;
            }
        }

        private void StatutPro(object sender, RoutedEventArgs e)
        {
            if (checkPro.IsChecked == true)
            {
                lbStatut.Content = "Pro";
                proMod = true;
            }
            else
            {
                lbStatut.Content = "Amateur";
                proMod = false;
            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            if(flagPseudo == true || pseudo_existe == false)
            {
                string combinedToBeRemoved = nom + "#" + prenom + "#" + pseudo + "#" + nationalite + "#" + jeu + "#" + pro + "#" + points + "#" + mail + "#" + imgPath;
                int indexToBeRemoved = 0;
                string combinedMod = nomMod + "#" + prenomMod + "#" + pseudoMod + "#" + nationaliteMod + "#" + jeuMod + "#" + proMod + "#" + pointsMod + "#" + mailMod + "#" + @"..\..\pictures\" + pseudoMod + extensionMod;

                imgPathFinalMod = AppDomain.CurrentDomain.BaseDirectory + @"..\..\pictures\" + pseudoMod + extensionMod;
                if(pseudo == pseudoMod && flagImage == true)
                {
                    File.Delete(imgPath);
                    File.Copy(imgPathAbsoluteMod, imgPathFinalMod);
                }
                
                if (pseudo != pseudoMod && flagImage== true)
                {
                    File.Delete(imgPath);
                }

                if (pseudo != pseudoMod && flagImage == false)
                {
                    File.Move(imgPath , AppDomain.CurrentDomain.BaseDirectory + @"..\..\pictures\" + pseudoMod + extensionMod);
                }


                List<string> lignes = File.ReadAllLines(filePath).ToList();
                foreach (string ligne in lignes)
                {
                    if (ligne.Contains(combinedToBeRemoved) == true)
                    {
                        break;
                    }
                    indexToBeRemoved++;
                }
                lignes.RemoveAt(indexToBeRemoved);
                lignes.Add(combinedMod);
                File.WriteAllLines(filePath, lignes);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ce pseudo est déjà utilisé", "Informations", MessageBoxButton.OK);
            }
            
        }
        private void cbSelection(object sender, SelectionChangedEventArgs e)
        {
            if (cbJeu.SelectedItem != null)
            {
                lbJeu.Content = cbJeu.SelectedItem;
                jeuMod = lbJeu.Content.ToString();
            }
            else
            {
                lbJeu.Content = jeu;
                jeuMod = jeu;
            }
        }

        

        private void Window_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
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
