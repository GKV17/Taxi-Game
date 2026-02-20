using System;
using System.Collections.Generic;
using System.Linq;
using TaxiGame.Core.Models;

namespace TaxiGame.Core.Services
{
    public class JeuService
    {
        public List<Joueur> Joueurs { get; private set; }
        public Plateau Plateau { get; private set; }
        public int JoueurActuelIndex { get; private set; }
        public int Tour { get; private set; }
        public string NomPartie { get; private set; }

        private Random _random;

        public JeuService()
        {
            _random = new Random();
            Plateau = new Plateau();
        }

        public void InitialiserPartie(string nomPartie, List<Joueur> joueurs)
        {
            NomPartie = nomPartie;
            Joueurs = joueurs;
            JoueurActuelIndex = 0;
            Tour = 1;

            foreach (var joueur in Joueurs)
            {
                joueur.BonusMiParcoursObtenu = false;
                joueur.BonusDerniereLigneObtenu = false;
                
            }
        }

        public ResultatTour JouerTour()
        {
            var joueur = Joueurs[JoueurActuelIndex];
            int de = _random.Next(1, 7);


            joueur.Position += de;
            joueur.Score += de;

            int nombreCases=Plateau.GetNombreCases();
            if (joueur.Position > 50)
                joueur.Position = 50;

            // Points basiques
            if (de == 6) joueur.Score += 15; // Bonus pour un 6
            joueur.Score += 5; // Points par tour

            if (joueur.Position >= 25 && !joueur.BonusMiParcoursObtenu)
            {
                joueur.Score += 25;
                joueur.BonusMiParcoursObtenu = true;
            }

            if (joueur.Position >= 40 && !joueur.BonusDerniereLigneObtenu)
            {
                joueur.Score += 10;
                joueur.BonusDerniereLigneObtenu = true;
            }

            var resultat = new ResultatTour
            {
                Joueur = joueur,
                DeValue = de,
                CaseArrivee = Plateau.GetCase(joueur.Position - 1),
                Message = $"{joueur.Nom} avance de {de} cases"
            };


            // Passer au joueur suivant
            JoueurActuelIndex = (JoueurActuelIndex + 1) % Joueurs.Count;
            if (JoueurActuelIndex == 0) Tour++;

            return resultat;
        }
        
        public bool EstPartieTerminee()
        {   
            int nombreCases=Plateau.GetNombreCases();
            return Joueurs.Any(j => j.Position >= nombreCases);
        }

        public Joueur GetGagnant()
        {
            return Joueurs.OrderByDescending(j => j.Position)
                         .First();
        }
        public List<(Joueur joueur, int score, int rang)> GetClassementJoueurs()
        {
            var joueursTries = Joueurs
                .OrderByDescending(j => j.Position)
                .ThenByDescending(j => j.Score) 
                .ToList();

            var classement = new List<(Joueur joueur, int Score, int rang)>();

            for (int i = 0; i < joueursTries.Count; i++)
            {
                classement.Add((joueursTries[i], joueursTries[i].Score, i + 1));
            }

            return classement;
        }

    } 

}