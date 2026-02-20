using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;
using TaxiGame.Core.Models;
using TaxiGame.Core.Services;

namespace TaxiGame.WPF
{
    public partial class MainWindow : Window
    {
        private JeuService _jeuService;
        private List<Brush> _couleurs = new List<Brush>
        {
            Brushes.Red,
            Brushes.Blue,
            Brushes.Green,
            Brushes.Purple,
            Brushes.Yellow,
            Brushes.Orange
        };

        public MainWindow()
        {
            InitializeComponent();
            _jeuService = new JeuService();
            AjouterJoueurInitial();
        }

        private void AjouterJoueurInitial()
        {
            for (int i = 0; i < 2; i++)
            {
                AjouterJoueurConfig($"Joueur {i + 1}", _couleurs[i]);
            }
        }

        private void AjouterJoueurConfig(string nom, Brush couleur)
        {
            var panel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 2, 0, 2) };

            var ellipse = new Ellipse
            {
                Width = 15,
                Height = 15,
                Fill = couleur,
                Margin = new Thickness(0, 0, 5, 0)
            };

            var textBox = new TextBox
            {
                Text = nom,
                Width = 100,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Children.Add(ellipse);
            panel.Children.Add(textBox);
            joueursConfigPanel.Children.Add(panel);
        }

        private void AjouterJoueur_Click(object sender, RoutedEventArgs e)
        {

            if (joueursConfigPanel.Children.Count < 6 )
            {
                int index = joueursConfigPanel.Children.Count;
                AjouterJoueurConfig($"Joueur {index + 1}", _couleurs[index % _couleurs.Count]);
            }

           
        }

        private void SupprimerJoueur_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier s'il y a au moins 2 joueurs (minimum pour jouer)
            if (joueursConfigPanel.Children.Count > 2)
            {
                // Supprimer le dernier joueur ajouté
                joueursConfigPanel.Children.RemoveAt(joueursConfigPanel.Children.Count - 1);

                AjouterMessage($"Joueur supprimé. Il reste {joueursConfigPanel.Children.Count} joueur(s).");
            }
            else
            {
                MessageBox.Show("Il doit y avoir au moins 2 joueurs pour jouer !",
                               "Nombre minimum de joueurs",
                               MessageBoxButton.OK,
                               MessageBoxImage.Warning);
            }
        }

        private void NouvellePartie_Click(object sender, RoutedEventArgs e)
        {
            var joueurs = new List<Joueur>();

            for (int i = 0; i < joueursConfigPanel.Children.Count; i++)
            {
                var panel = (StackPanel)joueursConfigPanel.Children[i];
                var textBox = (TextBox)panel.Children[1];
                var ellipse = (Ellipse)panel.Children[0];

                joueurs.Add(new Joueur(
                    textBox.Text,
                    ellipse.Fill.ToString()
                ));
            }

            string nomPartie = txtNomPartie.Text;
            _jeuService.InitialiserPartie(nomPartie, joueurs);

            btnAjouterJoueur.IsEnabled = true;
            btnSupprimerJoueur.IsEnabled = true;
            btnJouer.IsEnabled = true;
            MettreAJourInterface();
            AjouterMessage("Nouvelle partie démarrée !");
            btnNouvpa.IsEnabled = false;

            
        }

        private void JouerTour_Click(object sender, RoutedEventArgs e)
        {
            if (_jeuService.Joueurs == null || _jeuService.EstPartieTerminee())
                return;

            var resultat = _jeuService.JouerTour();

            AjouterMessage($"{resultat.Joueur.Nom} lance le dé: {resultat.DeValue}");
            AjouterMessage($"  → Arrivé à: {resultat.CaseArrivee}");

            MettreAJourInterface();


            if (_jeuService.EstPartieTerminee())
            {
                var gagnant = _jeuService.GetGagnant();
                AjouterMessage($"{gagnant.Nom} a gagné !");

                
                AjouterMessage("\n CLASSEMENT FINAL ");
                var classement = _jeuService.GetClassementJoueurs();
                foreach (var (joueur, score, rang) in classement)
                {
                    AjouterMessage($"{rang}. Position: {joueur.Position} , ({joueur.Nom} - {score} points)");
                }

                btnJouer.IsEnabled = false;
                btnNouvpa.IsEnabled = true;
            }
        }


        private void MettreAJourInterface()
        {
            if (_jeuService.Joueurs == null) return;

            btnAjouterJoueur.IsEnabled = false;
            btnSupprimerJoueur.IsEnabled = false;

            txtTourInfo.Text = $"Tour {_jeuService.Tour} - " +
                              $"{_jeuService.Joueurs[_jeuService.JoueurActuelIndex].Nom} joue";

            // Afficher le plateau
            plateauPanel.Children.Clear();

            int nombreCases = _jeuService.Plateau.GetNombreCases();

            for (int i = 0; i < nombreCases; i++)
            {
                var border = new Border
                {
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    Width = 100,
                    Height = 60,
                    Margin = new Thickness(2),
                    Background = i == 0 ? Brushes.Cyan :
                                i == 9 ? Brushes.LightGreen :
                                i == 19 ? Brushes.LightGreen :
                                i == 29 ? Brushes.LightGreen :
                                i == 39 ? Brushes.LightGreen :
                                i == 49 ? Brushes.Orange :
                                Brushes.WhiteSmoke
                };

                var stack = new StackPanel();
                stack.Children.Add(new TextBlock
                {
                    Text = $"{i + 1}",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontWeight = FontWeights.Bold
                });

                stack.Children.Add(new TextBlock
                {
                    Text = _jeuService.Plateau.Cases[i],
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    FontSize = 8,
                    Margin = new Thickness(1)
                });

                // Marquer les joueurs sur cette case
                foreach (var joueur in _jeuService.Joueurs)
                {
                    if (joueur.Position == i +1)
                    {
                        var joueurMarket = new Ellipse
                        {
                            Width = 10,
                            Height = 10,
                            Fill = (Brush)new BrushConverter().ConvertFromString(joueur.Couleur),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Margin = new Thickness(0, 1, 0, 0)
                        };
                        stack.Children.Add(joueurMarket);
                    }
                }

                border.Child = stack;
                plateauPanel.Children.Add(border);
            }
        }

        private void Sauvegarder_Click(object sender, RoutedEventArgs e)
        {
            if (_jeuService.Joueurs == null) return;

            var dialog = new SaveFileDialog
            {
                Filter = "Fichier Taxi (*.json)|*.json",
                FileName = $"{_jeuService.NomPartie}.json"
            };

            if (dialog.ShowDialog() == true)
            {
                var etat = new EtatJeu
                {
                    NomPartie = _jeuService.NomPartie,
                    Joueurs = _jeuService.Joueurs,
                    JoueurActuelIndex = _jeuService.JoueurActuelIndex,
                    Tour = _jeuService.Tour,
                    DateSauvegarde = DateTime.Now
                };

                var sauvegarde = new SauvegardeService();
                sauvegarde.Sauvegarder(dialog.FileName, etat);
                AjouterMessage("Partie sauvegardée");
            }
        }

        private void ChargerPartie_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Fichier Taxi (*.json)|*.json"
            };

            if (dialog.ShowDialog() == true)
            {
                var sauvegarde = new SauvegardeService();
                var etat = sauvegarde.Charger(dialog.FileName);

                _jeuService.InitialiserPartie(etat.NomPartie, etat.Joueurs);
          
                btnJouer.IsEnabled = true;
                btnNouvpa.IsEnabled = false;
                MettreAJourInterface();
                AjouterMessage("Partie chargée");
            }

            btnAjouterJoueur.IsEnabled = false;
            btnSupprimerJoueur.IsEnabled = false;
            btnCharger.IsEnabled = false;

            
        }

        private void Quitter_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Êtes-vous sûr de vouloir quitter le jeu ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void AjouterMessage(string message)
        {
            txtJournal.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            txtJournal.ScrollToEnd();
        }
    }
}