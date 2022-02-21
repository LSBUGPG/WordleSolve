using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordList
{
    readonly int size;
    readonly WordSet [,] wordsWithLetterCount;
    readonly WordSet [,] wordsWithLetterPlace;

    public static int CountLetter(string word, char letter)
    {
        int count = 0;
        foreach (char c in word)
        {
            if (c == letter)
            {
                ++count;
            }
        }
        return count;
    }

    public WordList(List<string> list)
    {
        int count = list.Count;
        Debug.Assert(count > 0, "must supply a non-empty list");
        size = 5;
        wordsWithLetterPlace = new WordSet [size, Histogram.alphabet];
        wordsWithLetterCount = new WordSet [size + 1, Histogram.alphabet];
        for (int a = 0; a < Histogram.alphabet; ++a)
        {
            for (int i = 0; i < size; ++i)
            {
                wordsWithLetterPlace[i, a] = new WordSet(count, false);
            }

            for (int i = 0; i < size + 1; ++i)
            {
                wordsWithLetterCount[i, a] = new WordSet(count, false);
            }
        }

        for (int w = 0; w < count; ++w)
        {
            string word = list[w];
            for (int i = 0; i < size; ++i)
            {
                char letter = word[i];
                wordsWithLetterPlace[i, Histogram.IndexFromLetter(letter)].Add(w);
            }
        }

        for (int w = 0; w < count; ++w)
        {
            string word = list[w];
            for (char a = 'a'; a <= 'z'; ++a)
            {
                int n = CountLetter(word, a);
                wordsWithLetterCount[n, Histogram.IndexFromLetter(a)].Add(w);
            }
        }
    }

    public WordSet WordsWithLetterInPlace(char c, int position)
    {
        return wordsWithLetterPlace[position, Histogram.IndexFromLetter(c)];
    }

    public WordSet WordsWithLetterCount(char c, int count)
    {
        return wordsWithLetterCount[count, Histogram.IndexFromLetter(c)];
    }
}
