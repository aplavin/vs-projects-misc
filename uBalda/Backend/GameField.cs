using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace uBalda.Backend
{
    public class GameField
    {
        public int Size { get; private set; }
        public char[,] field { get; private set; }

        public GameField(int size, string initWord)
        {
            if (initWord.Length != size)
            {
                throw new ArgumentException("Word length doesn't match field size", "size, initWord");
            }

            Size = size;

            field = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                field[(size - 1) / 2, i] = initWord[i];
            }
        }

        public LetterNumbers GetLetterNumbers()
        {
            return new LetterNumbers(field);
        }

        public bool IsEmpty(Cell cell)
        {
            return IsEmpty(cell.x, cell.y);
        }

        public bool IsEmpty(int x, int y)
        {
            return field[x, y] == 0;
        }

        public bool HasNeighbours(Cell cell)
        {
            foreach (Cell c in cell.Neighbours)
            {
                if (!IsEmpty(c))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasNeighbours(int x, int y)
        {
            return HasNeighbours(new Cell(x, y));
        }

        public char this[int x, int y]
        {
            get { return this[new Cell(x, y)]; }
            set { this[new Cell(x, y)] = value; }
        }

        public char this[Cell cell]
        {
            get
            {
                if (cell.Exists(Size))
                {
                    return field[cell.x, cell.y];
                }
                else
                {
                    throw new ArgumentOutOfRangeException("cell", "Cell is out of the field bounds");
                }
            }

            set
            {
                if (cell.Exists(Size))
                {
                    field[cell.x, cell.y] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("cell", "Cell is out of the field bounds");
                }
            }
        }
    }
}
