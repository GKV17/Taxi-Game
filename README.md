# Taxi Game - README

## Description

**Taxi Game** est un jeu de plateau numérique où plusieurs joueurs s'affrontent sur un plateau de 50 cases. Chaque joueur lance un dé, avance sur le plateau et accumule des points grâce à des bonus (mi-parcours, derniere ligne, etc.).

Le but est d'arriver le premier a la case **50 (Aeroport International)** tout en maximisant son score.

Le jeu inclut :
- Sauvegarde et chargement de partie (JSON)
- Classement final
- Interface graphique avec WPF

## Fonctionnalites

- Jusqu'a 6 joueurs (minimum 2)
- Plateau dynamique avec 50 lieux thematiques (Gare, Hopital, Musee, etc.)
- Bonus speciaux :
  - +15 points pour un 6
  - +25 points a la mi-parcours (case 25)
  - +10 points a la derniere ligne (case 40)
- Sauvegarde / Chargement de partie
- Journal de bord en temps reel
- Classement detaille en fin de partie
- Interface moderne

## Technologies utilisees

- C# .NET 8.0
- WPF (Windows Presentation Foundation)
- MVVM Light (architecture)
- System.Text.Json pour la sauvegarde
- Projet separe : TaxiGame.Core (logique metier) et TaxiGame.WPF (interface)

## Comment jouer

1. **Nouvelle Partie**
   - Entrez le nom de la partie
   - Ajoutez ou supprimez des joueurs (2 a 6)
   - Cliquez sur "Nouvelle Partie"

2. **Pendant le jeu**
   - Cliquez sur "Jouer le tour" pour lancer le de
   - Les joueurs jouent a tour de role automatiquement
   - Les pions (cercles colores) se deplacent sur le plateau

3. **Fin de partie**
   - Le premier a atteindre la case 50 gagne
   - Le classement final s'affiche

4. **Sauvegarde**
   - Cliquez sur "Sauvegarder" pour enregistrer la partie en .json
   - Utilisez "Charger" pour reprendre une partie sauvegardee

## Structure du projet

```
Taxi-Game-main/
├── TaxiGame.Core/          ← Logique du jeu (modeles + services)
│   ├── Joueur.cs
│   ├── Plateau.cs
│   ├── JeuService.cs
│   ├── SauvegardeService.cs
│   └── ...
├── TaxiGame.WPF/           ← Interface graphique
│   ├── MainWindow.xaml
│   └── MainWindow.xaml.cs
├── TaxiGame.sln
└── README.md
```



