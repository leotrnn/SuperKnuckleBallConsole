using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace superKnuckleBallsConsole
{
    internal class Dice
    {
        // Constantes..
        const int VAL_MAX = 6;
        const int VAL_MIN = 0;

        // Champs...
        private int _valeur;
        private Random _rd;

        // Propriétés...
        public int Valeur
        {
            get { return _valeur; }
            set
            {
                _valeur = value;
                if (value < VAL_MIN)
                    _valeur = VAL_MIN;
                if (value > VAL_MAX)
                    _valeur = VAL_MAX;
            }
        }

        public Random Rd { get => _rd; set => _rd = value; }

        // Constructeurs...
        public Dice(int valeurInitiale)
        {
            this.Valeur = valeurInitiale;
            this.Rd = new Random(Guid.NewGuid().GetHashCode());
        }
        public Dice()
        : this(VAL_MAX)
        {
            // Appel du constructeur désigné...
        }
        // Méthodes...
        public void Roule()
        {
            this.Valeur = this.Rd.Next(VAL_MIN + 1, VAL_MAX + 1);
        }
        public override string ToString()
        {
            return this.Valeur.ToString();
        }
    }
}
