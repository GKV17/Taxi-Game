using System.IO;
using System.Text.Json;
using TaxiGame.Core.Models;

namespace TaxiGame.Core.Services
{
    public class SauvegardeService
    {
        public void Sauvegarder(string chemin, EtatJeu etat)
        {
            string json = JsonSerializer.Serialize(etat, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(chemin, json);
        }

        public EtatJeu Charger(string chemin)
        {
            string json = File.ReadAllText(chemin);
            return JsonSerializer.Deserialize<EtatJeu>(json);
        }
    }
}