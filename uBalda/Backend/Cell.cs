using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace uBalda.Backend
{
    public class Cell : IEquatable<Cell>
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public Cell[] Neighbours
        {
            get { return new Cell[] { new Cell(x, y + 1), new Cell(x, y - 1), new Cell(x + 1, y), new Cell(x - 1, y) }; }
        }

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsNeighbour(Cell c)
        {
            return (c.x == x && c.y == y + 1
                || c.x == x && c.y == y - 1
                || c.x == x + 1 && c.y == y
                || c.x == x - 1 && c.y == y);
        }

        public bool Exists(int size)
        {
            return (x >= 0 && x < size
                && y >= 0 && y < size);
        }

        public static Cell operator +(Cell c1, Cell c2)
        {
            return new Cell(c1.x + c2.x, c1.y + c2.y);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", x, y);
        }

        #region IEquatable<Cell> Members

        public bool Equals(Cell c)
        {
            return (x == c.x && y == c.y);
        }

        #endregion
    }
}
