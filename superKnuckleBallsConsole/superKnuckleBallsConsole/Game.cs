using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace superKnuckleBallsConsole
{
    internal class Game
    {
        private List<Player> _players;
        private int _indexActualPlayer;

        public int IndexActualPlayer
        {
            get => _indexActualPlayer;
            set
            {
                if (value >= Players.Count)
                {
                    _indexActualPlayer = 0;
                }
                else
                {
                    _indexActualPlayer = value;
                }
            }
        }


        internal List<Player> Players { get => _players; set => _players = value; }

        public Game(List<Player> players)
        {
            Players = players;
            IndexActualPlayer = 0;
        }

        public void NextPlayer()
        {
            IndexActualPlayer++;
        }

        public bool GameEnd()
        {
            // vérifie si 1 des deux joueurs n'a plus de case libre
            return (!Players[0].DicesPlayer.Any(dice => dice.Valeur == 0) || !Players[1].DicesPlayer.Any(dice => dice.Valeur == 0));
        }

        public void Round(int selectedColumn, Dice dice)
        {
            int[] columnIndices;

            // Déterminer les indices en fonction de la colonne sélectionnée
            switch (selectedColumn)
            {
                case 1:
                    columnIndices = new int[] { 6, 3, 0 };
                    break;
                case 2:
                    columnIndices = new int[] { 7, 4, 1 };
                    break;
                case 3:
                    columnIndices = new int[] { 8, 5, 2 };
                    break;
                default:
                    Console.WriteLine("Colonne invalide !");
                    return;
            }

            // Chercher la première position vide de bas en haut dans la colonne sélectionnée
            foreach (int index in columnIndices)
            {
                if (Players[IndexActualPlayer].DicesPlayer[index].Valeur == 0)
                {
                    Players[IndexActualPlayer].DicesPlayer[index] = dice;
                    break;
                }
            }

            // Vérifier si l'autre joueur a posé un dé avec la même valeur dans la colonne sélectionnée
            int otherPlayerIndex = (IndexActualPlayer == 0) ? 1 : 0;

            // Vérifier tous les indices de colonne pour supprimer les dés correspondants chez l'adversaire
            foreach (int index in columnIndices)
            {
                if (Players[otherPlayerIndex].DicesPlayer[index].Valeur == dice.Valeur)
                {
                    // Supprimer le dé correspondant chez l'adversaire
                    Players[otherPlayerIndex].DicesPlayer[index].Valeur = 0;

                    // Déplacer les dés au-dessus vers le bas
                    MoveDicesDown(otherPlayerIndex, columnIndices);
                }
            }
        }

        // Méthode pour déplacer les dés vers le bas dans la colonne
        private void MoveDicesDown(int playerIndex, int[] columnIndices)
        {
            for (int i = 0; i < columnIndices.Length; i++)
            {
                int currentIndex = columnIndices[i];

                // Si le dé courant est 0 (supprimé), déplacez les dés au-dessus vers le bas
                if (Players[playerIndex].DicesPlayer[currentIndex].Valeur == 0)
                {
                    // Cherche un dé au-dessus pour le déplacer
                    for (int j = i + 1; j < columnIndices.Length; j++)
                    {
                        int upperIndex = columnIndices[j];

                        if (Players[playerIndex].DicesPlayer[upperIndex].Valeur > 0)
                        {
                            // Déplacez le dé au-dessous
                            Players[playerIndex].DicesPlayer[currentIndex].Valeur = Players[playerIndex].DicesPlayer[upperIndex].Valeur;
                            Players[playerIndex].DicesPlayer[upperIndex].Valeur = 0; // Supprimez le dé au-dessus
                            break; // Sortir de la boucle après le déplacement
                        }
                    }
                }
            }
        }




        public int SumPoints(int indexPlayer)
        {
            Player player = Players[indexPlayer];
            int totalPoints = 0;

            // Définir les indices pour chaque colonne
            int[][] columnIndices = new int[][]
            {
                new int[] { 6, 3, 0 }, // Colonne 1
                new int[] { 7, 4, 1 }, // Colonne 2
                new int[] { 8, 5, 2 }  // Colonne 3
            };

            // Parcourir chaque colonne
            foreach (var indices in columnIndices)
            {
                // Obtenir les valeurs des dés dans la colonne
                var diceValues = indices.Select(index => player.DicesPlayer[index].Valeur).ToList();

                // Compter les occurrences des valeurs
                var groupedValues = diceValues.GroupBy(value => value)
                                               .Where(g => g.Key > 0) // Ignorer les valeurs 0
                                               .ToDictionary(g => g.Key, g => g.Count());

                // Appliquer les multiplicateurs
                foreach (var pair in groupedValues)
                {
                    int value = pair.Key;
                    int count = pair.Value;

                    // Ajouter des points en fonction du nombre de dés de cette valeur
                    if (count == 2)
                    {
                        totalPoints += value * 3; // Multiplier par 3 si 2 dés de cette valeur
                    }
                    else if (count >= 3)
                    {
                        totalPoints += value * 6; // Multiplier par 6 si 3 dés de cette valeur
                    }
                    else
                    {
                        totalPoints += value; // Pas de multiplicateur si 0 ou 1 dé
                    }
                }
            }

            return totalPoints;
        }

        public void DeclareWinner()
        {
            int player1Points = SumPoints(0);
            int player2Points = SumPoints(1);

            Console.WriteLine(
                player1Points > player2Points
                    ? $"Game over! Player 1 wins with {player1Points} points!"
                    : player2Points > player1Points
                        ? $"Game over! Player 2 wins with {player2Points} points!"
                        : $"Game over! It's a tie with both players having {player1Points} points!"
            );
        }





    }
}
