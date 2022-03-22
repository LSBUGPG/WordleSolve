using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{
    public Letter prefab;
    public int size = 5;
    List<Letter> letters;

    private void Awake()
    {
        letters = new List<Letter>();
        for (int i = 0; i < size; ++i)
        {
            letters.Add(Instantiate(prefab, transform));
        }
    }

    public void SetWord(string word)
    {
        for (int i = 0; i < word.Length; ++i)
        {
            letters[i].SetLetter(word[i]);
        }
    }

    internal void SetMatchedLetter(int i)
    {
        letters[i].SetColour(Color.green);
    }

    internal void SetContainingLetter(int i)
    {
        letters[i].SetColour(Color.yellow);
    }

    internal void SetNotContainingLetter(int i)
    {
        letters[i].SetColour(Color.grey);
    }
}
