using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace uBalda.Backend
{
    class HumanPlayer
    {
        private List<Move> moves;

        public HumanPlayer()
        {
            moves = new List<Move>();
        }

        public int Score
        {
            get
            {
                // return sum of all words length
                return moves.Aggregate(0, (sum, move) => sum + move.word.Length);
            }
        }

        public void AddMove(Move move)
        {
            moves.Add(move);
        }
    }
}
