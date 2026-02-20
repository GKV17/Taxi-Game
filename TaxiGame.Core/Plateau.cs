using System.Collections.Generic;

namespace TaxiGame.Core.Models
{
    public class Plateau
    {
        public List<string> Cases { get; set; }

        public Plateau()
        {
            Cases = new List<string>();

            string[] lieux={
                "Gare Centrale", "Place du Marché", "École Primaire", "Hôpital", "Bibliothèque",
                "Parc Municipal", "Mairie", "Stade", "Centre Commercial", "Théâtre",
                "Musée d'Art", "Restaurant Le Gourmet", "Cinéma", "Piscine Municipale", "Gymnase",
                "Banque", "Poste", "Commissariat", "Caserne de Pompiers", "Église",
                "Université", "Laboratoire", "Zoo", "Jardin Botanique", "Planétarium",
                "Port de Plaisance", "Aéroport Régional", "Gare Routière", "Terminal Ferry", "Station de Métro",
                "Château", "Monument Historique", "Site Archéologique", "Observatoire", "Tour de Télévision",
                "Centre de Congrès", "Hôtel de Ville", "Palais de Justice", "Ambassade", "Consulat",
                "Quartier des Affaires", "Place des Festivals", "Marché aux Fleurs", "Rue Piétonne", "Promenade",
                "Plage", "Port de Pêche", "Phare", "Montagne", "Arrivée - Aéroport International"
            };

            for(int i = 0; i < lieux.Length; i++)
            {
                Cases.Add(lieux[i]);    
            }

            if (Cases.Count > 50)
            {
                Cases.GetRange(0, 50);
            }
            
        }

        public string GetCase(int position)
        {
            if (position >= 0 && position < Cases.Count)
                return Cases[position];
            return "Hors plateau";
        }
        public int GetNombreCases()
        {
            return Cases.Count;
        }
    }
}