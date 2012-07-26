using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace uBalda.Backend
{
    public class Vocabulary
    {
        private static Random random = new Random();
        private readonly string fileName = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName),
                "dict.txt");
        private string[] vocabulary;
        private List<string> usedWords;

        public Vocabulary()
        {
            usedWords = new List<string>();

            List<string> t = new List<string>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    t.Add(reader.ReadLine().Replace("-", "").Replace('ё', 'е').ToLower());
                }
            }

            vocabulary = t.ToArray();
            Array.Sort(vocabulary, (s1, s2) => s2.Length.CompareTo(s1.Length));
        }

        public void AddUsed(string word)
        {
            usedWords.Add(word);
        }

        public string RandomWord(int length)
        {
            var words = (from s in vocabulary
                         where s.Length == length
                         select s)
                         .Except(usedWords)
                         .ToArray();
            if (words.Length == 0)
            {
                throw new ArgumentOutOfRangeException("size", "No words with length = " + length + " are in the dictionary.");
            }
            return words[random.Next(words.Length)];
        }

        public bool WordExists(string word)
        {
            return vocabulary.Contains(word);
        }

        public bool IsUsed(string word)
        {
            return usedWords.Contains(word);
        }

        public string[] GetWords(int maxLength)
        {
            return (from word in vocabulary
                    where word.Length <= maxLength && !IsUsed(word)
                    select word)
                    .ToArray();
        }
    }
}
