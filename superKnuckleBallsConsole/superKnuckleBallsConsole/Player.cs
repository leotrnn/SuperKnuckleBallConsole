using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace superKnuckleBallsConsole
{
    internal class Player
    {
        private int _idPlayer;
        private string _namePlayer;
        private List<Dice> _dicesPlayer;

        public int IdPlayer { get => _idPlayer; set => _idPlayer = value; }
        public string NamePlayer { get => _namePlayer; set => _namePlayer = value; }
        internal List<Dice> DicesPlayer { get => _dicesPlayer; set => _dicesPlayer = value; }

        public Player(int idPlayer, string namePlayer)
        {
            IdPlayer = idPlayer;
            NamePlayer = namePlayer;
            DicesPlayer = new List<Dice>();

            // Initialiser la position des dés
            for (int i = 0; i < 9; i++)
            {
                DicesPlayer.Add(new Dice(0));
            }
        }
    }
}
