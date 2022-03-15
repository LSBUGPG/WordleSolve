using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Histogram
{
    public int [] letters;

    public Histogram(string word)
    {
        letters = new int [26];
        foreach (var letter in word)
        {
            letters[letter - 'a']++;
        }
    }

    public int LetterCount(char letter)
    {
        return letters[letter - 'a'];
    }
}
