using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hackathon
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Joueur> listeJoueur = new ObservableCollection<Joueur>();
        string filePath = "../../database/listeJoueur.txt";
        int currentRow , recherche_ligne = 0;

        public MainWindow()
        {
            InitializeComponent();
            Affichage_DataGrid();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dt_grid.ItemsSource = listeJoueur;
        }

        private void Recherche_joueur(object sender, RoutedEventArgs e)
        {
            bool IsFound = false;
            List<string> lines = File.ReadAllLines(filePath).ToList();
            foreach (string line in lines)
            {
                string[] entries = line.Split('#');
                if (entries[2] == recherche_joueur_txt.Text)
                {
                    ProfilJoueur profil = new ProfilJoueur(entries[0], entries[1], entries[2], entries[3], entries[4], entries[5], entries[6] , entries[7] , entries[8]);
                    profil.Show();
                    IsFound = true;
                }
            }
            if (IsFound == false)
            {
                MessageBox.Show("Veuillez entrez un pseudo exact existant", "Pseudo introuvable", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Ajout_joueur(object sender, RoutedEventArgs e)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            AjoutJoueur profil = new AjoutJoueur(filePath , lines);
            profil.Show();
            this.Close();
        }
        private void Modifier_Joueur(object sender, RoutedEventArgs e)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            if (File.ReadAllText(filePath) == "")
            {
                MessageBox.Show("Aucun joueur n'a été créé", "DataBase null", MessageBoxButton.OK , MessageBoxImage.Information);
            }
            foreach (string line in lines)
            {
                if (recherche_ligne == currentRow) // Test afin de sélectionner la bonne ligne
                {
                    ModifJoueur profil = new ModifJoueur(filePath, line , lines);
                    profil.Show();
                    this.Close();
                }
                recherche_ligne++;
            }
            recherche_ligne = 0;
        }
        private void Supprimer_joueur(object sender, RoutedEventArgs e)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();

            if (File.ReadAllText(filePath) == "")
            {
                MessageBox.Show("Joueur inexistant", "DataBase null", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            foreach (string line in lines)
            {
                if (recherche_ligne == currentRow) // Test afin de sélectionner la bonne ligne
                {
                    string[] entries = line.Split('#');
                    MessageBoxResult resultMessageBox = MessageBox.Show("Vous allez effacer le joueur : [ "+ entries[2]+ " ]" +"\nMerci de confirmer", "Demande de confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (resultMessageBox == MessageBoxResult.OK) // Confirmation de la suppression du joueur
                    {
                        Supprimer_ligneEtActualisation(line , entries[8]);
                    }
                }
                recherche_ligne++;
            }
            recherche_ligne = 0;
        }


        private void Selection_rows(object sender, SelectionChangedEventArgs e)
        {
            // Retourne la ligne sélectionnée dans le DataGrid
            var ligneSelection = dt_grid.Items.IndexOf(dt_grid.SelectedItem);
            currentRow = ligneSelection;
            // Le met dans la recherche de joueur
            List<string> lines = File.ReadAllLines(filePath).ToList();
            foreach (string line in lines)
            {
                if (recherche_ligne == currentRow) // Test afin de sélectionner la bonne ligne
                {
                    string[] entries = line.Split('#');
                    recherche_joueur_txt.Text = entries[2];
                }
                recherche_ligne++;
            }
            recherche_ligne = 0;

        }

        

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Return == e.Key) //Si Touche "ENTER" du clavier => Fais la recherche
            {
                Recherche_joueur(sender, e);
            }
            
        }
        private void dt_grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Return == e.Key) //Si Touche "ENTER" du clavier => Fais la recherche
            {
                Recherche_joueur(sender, e);
            }
        }


        // Mes Méthodes
        private void Affichage_DataGrid()
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            foreach (string line in lines)
            {
                string[] entries = line.Split('#');
                Joueur newClient = new Joueur(entries[0], entries[1], entries[2], entries[3], entries[4], entries[5], entries[6], entries[7]);
                listeJoueur.Add(newClient);
            }
        }

        private void Supprimer_ligneEtActualisation(string line , string imagePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string texte = null;
            string ligneActuelle = null;
            // Ouverture du fichier
            while (sr.Peek() != -1) // tant qu'il n'est pas arrivé à la fin du fichier 
            {
                ligneActuelle = sr.ReadLine();
                if (!(ligneActuelle == line)) // Tant que la ligne n'est pas égale a celle que l'on veut supprimer
                {
                    texte += ligneActuelle + "\r\n";
                }
            }
            sr.Close();
            // Ré-écriture du fichier
            StreamWriter sr2 = new StreamWriter(filePath);
            sr2.Write(texte);
            sr2.Close();
            // Suppression image correspondante
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + imagePath);
            // Ré-affichage du dataGrid;
            listeJoueur.Clear();
            Affichage_DataGrid();
        }
    }
}
