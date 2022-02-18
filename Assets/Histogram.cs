using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Histogram
{
    public int [] letters;
    public static readonly int alphabet = 26;

    public static int IndexFromLetter(char letter)
    {
        int index = letter - 'a';
        Debug.Assert(index >= 0 && index < alphabet);
        return index;
    }

    public Histogram(string word)
    {
        letters = new int [alphabet];
        foreach (char letter in word)
        {
            letters[IndexFromLetter(letter)]++;
        }
    }

    public void Use(char letter)
    {
        letters[IndexFromLetter(letter)]--;
    }

    public bool Has(char letter)
    {
        return letters[IndexFromLetter(letter)] > 0;
    }
}