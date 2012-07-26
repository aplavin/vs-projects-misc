using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace uBalda.Backend
{
    public class Move
    {
        public string word { get; private set; }
        public CellPath path { get; private set; }

        public Move(string word, CellPath path)
        {
            if (word.Length != path.Length)
            {
                throw new ArgumentException("Word and path lengths are different", "word, path");
            }
        }
    }
}
