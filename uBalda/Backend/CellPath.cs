using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace uBalda.Backend
{
    public class CellPath
    {
        private List<Cell> path;


        public Cell Last
        {
            get
            {
                return path.Last();
            }
        }

        public Cell Get(int index)
        {
            return path.ElementAtOrDefault(index);
        }

        public int Length
        {
            get
            {
                return path.Count;
            }
        }

        public CellPath()
        {
            path = new List<Cell>();
        }

        public CellPath(int[,] used)
        {
            path = new List<Cell>();

            bool existMore = true;
            int next = 1;
            while (existMore)
            {
                for (int i = 0; i < used.GetLength(0); i++)
                {
                    for (int j = 0; j < used.GetLength(1); j++)
                    {
                        if (used[i, j] == next)
                        {
                            path.Add(new Cell(i, j));
                            used[i, j] = 0;
                            next++;
                        }
                        else if (used[i, j] > 0)
                        {
                            existMore = true;
                        }
                    }
                }
            }
        }

        public bool Add(Cell cell)
        {
            if (path.Contains(cell))
            {
                return false;
            }

            if (Length == 0 || cell.IsNeighbour(Last))
            {
                path.Add(cell);
                return true;
            }
            return false;
        }
    }
}
