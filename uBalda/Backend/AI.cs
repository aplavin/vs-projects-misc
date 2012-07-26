using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace uBalda.Backend
{
    class AI
    {
        private int size;
        private GameField gameField;
        private Vocabulary vocabulary;
        private char[,] field;
        private int[,] used;
        private Cell curCell;

        public AI(GameField gameField, Vocabulary vocabulary)
        {
            this.gameField = gameField;
            this.vocabulary = vocabulary;
            field = gameField.field;
            size = gameField.Size;
        }

        public Move GetMove()
        {          
            Cell[] candidates = FindCandidateCells();

            // calculate whole number of each letter in the field
            LetterNumbers fieldLetters = gameField.GetLetterNumbers();
            
            string[] words = vocabulary.GetWords(gameField.Size);

            var moves = new List<Move>();

            // into every cell from the list we found before...
            foreach (var cell in candidates)
            {
                curCell = cell;
                // try to add every letter of the alphabet...
                for (char ch = 'а'; ch <= 'я'; ch++)
                {
                    fieldLetters.AddLetter(ch);

                    field[cell.x, cell.y] = ch;
                    // and try to find a word on the field
                    foreach (string word in words)
                    {
                        // if we can place the word in the field - add the move to the list
                        if (fieldLetters.ContainsAll(new LetterNumbers(word)) && TryPlace(word))
                        {
                            moves.Add(new Move(word, new CellPath(used)));
                        }
                    }

                    fieldLetters.RemLetter(ch);
                }
                field[cell.x, cell.y] = (char)0;
            }

            moves = moves.Distinct().ToList();
            moves.Sort();

            moves.Sort((m1, m2) => m2.word.Length.CompareTo(m1.word.Length));
            return moves.First();
        }

        private Cell[] FindCandidateCells()
        {
            List<Cell> cells = new List<Cell>();
            for (int i = 0; i < gameField.Size; i++)
            {
                for (int j = 0; j < gameField.Size; j++)
                {
                    if (gameField.IsEmpty(i, j) && gameField.HasNeighbours(i, j))
                    {
                        cells.Add(new Cell(i, j));
                    }
                }
            }
            return cells.ToArray();
        }

        private bool TryPlace(string word)
        {
            used = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j] != 0 && TryPlace(word, i, j, 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool TryPlace(string word, int i, int j, int pos)
        {
            if (used[i, j] > 0)
            {
                return false;
            }
            if (field[i, j] != word[pos])
            {
                return false;
            }
            if (pos == word.Length - 1)
            {
                if (used[curCell.x, curCell.y] > 0)
                {
                    used[i, j] = pos + 1;
                    return true;
                }
                else
                {
                    used[i, j] = 0;
                    return false;
                }
            }
            used[i, j] = pos + 1;


            foreach (Cell c in new Cell(i, j).Neighbours)
            {
                if (c.Exists(size))
                {
                    if (TryPlace(word, c.x, c.y, pos + 1))
                    {
                        return true;
                    }
                }
            }

            used[i, j] = 0;
            return false;
        }
    }
}
