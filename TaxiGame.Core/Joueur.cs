namespace TaxiGame.Core.Models
{
    public class Joueur
    {
        public string Nom { get; set; }
        public int Position { get; set; }
        public int Score { get; set; }
        public string Couleur { get; set; }

        public bool BonusMiParcoursObtenu { get; set; }
        public bool BonusDerniereLigneObtenu { get; set; }

        public Joueur(string nom, string couleur)
        {
            Nom = nom;
            Couleur = couleur;
            Position =1;
            Score = 0;
            BonusMiParcoursObtenu = false;
            BonusDerniereLigneObtenu = false;
        }
    }
}