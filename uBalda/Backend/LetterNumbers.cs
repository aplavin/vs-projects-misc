using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace uBalda.Backend
{
    public class LetterNumbers
    {
        private Dictionary<char, int> data;

        public LetterNumbers()
        {
            data = new Dictionary<char, int>();
        }

        public LetterNumbers(string word)
        {
            data = new Dictionary<char, int>();
            foreach (char ch in word)
            {
                AddLetter(ch);
            }
        }

        public LetterNumbers(char[,] field)
        {
            data = new Dictionary<char, int>();
            foreach (char ch in field)
            {
                AddLetter(ch);
            }
        }

        public void AddLetter(char ch)
        {
            if (!data.ContainsKey(ch))
            {
                data.Add(ch, 1);
            }
            data[ch]++;
        }

        public void RemLetter(char ch)
        {
            data[ch]--;
        }

        public bool ContainsAll(LetterNumbers other)
        {
            if (other.data.Count > data.Count)
            {
                return false;
            }
            foreach (char ch in other.data.Keys)
            {
                if (!data.ContainsKey(ch) || data[ch] < other.data[ch])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
