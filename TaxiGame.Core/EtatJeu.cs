using System;
using System.Collections.Generic;

namespace TaxiGame.Core.Models
{
    public class EtatJeu
    {
        public string NomPartie { get; set; }
        public List<Joueur> Joueurs { get; set; }
        public int JoueurActuelIndex { get; set; }
        public int Tour { get; set; }
        public DateTime DateSauvegarde { get; set; }
    }
}
