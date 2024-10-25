using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace superKnuckleBallsConsole
{
    internal class Program
    {
        private static Game _SKBgame;
        internal static Game SKBGame { get => _SKBgame; set => _SKBgame = value; }

        public static void UpdateUI(Game game)
        {
            Console.Clear();

            Player player1 = game.Players[0];
            Player player2 = game.Players[1];

            string GDV(Dice dice) => dice.Valeur == 0 ? " " : dice.Valeur.ToString();

            Console.WriteLine($"{player1.NamePlayer}'s board - {game.SumPoints(0)}");
            Console.WriteLine($"+---+---+---+");
            Console.WriteLine($"| {GDV(player1.DicesPlayer[0])} | {GDV(player1.DicesPlayer[1])} | {GDV(player1.DicesPlayer[2])} |");
            Console.WriteLine($"+---+---+---+");
            Console.WriteLine($"| {GDV(player1.DicesPlayer[3])} | {GDV(player1.DicesPlayer[4])} | {GDV(player1.DicesPlayer[5])} |");
            Console.WriteLine($"+---+---+---+");
            Console.WriteLine($"| {GDV(player1.DicesPlayer[6])} | {GDV(player1.DicesPlayer[7])} | {GDV(player1.DicesPlayer[8])} |");
            Console.WriteLine($"+---+---+---+");
            Console.WriteLine();
            Console.WriteLine($"=============");
            Console.WriteLine();
            Console.WriteLine($"{player2.NamePlayer}'s board - {game.SumPoints(1)}");
            Console.WriteLine($"+---+---+---+");
            Console.WriteLine($"| {GDV(player2.DicesPlayer[0])} | {GDV(player2.DicesPlayer[1])} | {GDV(player2.DicesPlayer[2])} |");
            Console.WriteLine($"+---+---+---+");
            Console.WriteLine($"| {GDV(player2.DicesPlayer[3])} | {GDV(player2.DicesPlayer[4])} | {GDV(player2.DicesPlayer[5])} |");
            Console.WriteLine($"+---+---+---+");
            Console.WriteLine($"| {GDV(player2.DicesPlayer[6])} | {GDV(player2.DicesPlayer[7])} | {GDV(player2.DicesPlayer[8])} |");
            Console.WriteLine($"+---+---+---+");
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Player 1 name :");
            string p1Name = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Player 2 name : ");
            string p2Name = Console.ReadLine();

            List<Player> lstPlayers = new List<Player>
    {
        new Player(1, p1Name),
        new Player(2, p2Name)
    };

            SKBGame = new Game(lstPlayers);

            while (!SKBGame.GameEnd())
            {
                // Mise à jour de l'interface utilisateur
                UpdateUI(SKBGame);

                // Obtenir le joueur actuel
                Player currentPlayer = SKBGame.Players[SKBGame.IndexActualPlayer];
                Console.WriteLine($"{currentPlayer.NamePlayer}'s turn!");

                // Rouler un dé
                Dice dice = new Dice();
                dice.Roule();
                Console.WriteLine($"You rolled a {dice.Valeur}");

                // Demander au joueur de choisir une colonne
                int selectedColumn = 0;
                bool validInput = false;

                while (!validInput)
                {
                    Console.WriteLine("Choose a column to place your dice (1, 2, or 3):");

                    if (int.TryParse(Console.ReadLine(), out selectedColumn) && selectedColumn >= 1 && selectedColumn <= 3)
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
                                columnIndices = new int[] { };
                                break;
                        }

                        // Vérifier si la colonne est remplie
                        if (columnIndices.All(index => SKBGame.Players[SKBGame.IndexActualPlayer].DicesPlayer[index].Valeur != 0))
                        {
                            Console.WriteLine("This column is full! Please choose another column.");
                        }
                        else
                        {
                            validInput = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please choose a column (1, 2, or 3).");
                    }
                }

                // Placer le dé dans la colonne sélectionnée
                SKBGame.Round(selectedColumn, dice);

                // Passer au joueur suivant
                SKBGame.NextPlayer();
            }

            UpdateUI(SKBGame);

            // Afficher le message de fin de jeu
            SKBGame.DeclareWinner();
        }


    }
}
