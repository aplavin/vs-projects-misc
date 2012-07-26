using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace uBalda.Backend
{
    public class AIPlayer
    {
        private List<Move> moves;
        private GameField gameField;
        private Vocabulary vocabulary;

        public AIPlayer(GameField gameField)
        {
            moves = new List<Move>();
            this.gameField = gameField;
            vocabulary = new Vocabulary();
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

        public Move MakeMove()
        {
            AI ai = new AI(gameField, vocabulary);
            return ai.GetMove();
        }
    }
}
