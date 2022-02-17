using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    public enum Clue
    {
        NotSet,
        Unused,
        Used,
        Correct
    }

    public Clue [] clues;
    public string word;

    public Match(string word)
    {
        this.word = word;
        clues = new Clue [word.Length];
    }

    void ClearClues()
    {
        for (int i = 0; i < clues.Length; ++i)
        {
            clues[i] = Clue.NotSet;
        }
    }

    public void Set(string start)
    {
        this.word = start;
        ClearClues();
    }

    public void Score(string target)
    {
        ClearClues();
        Histogram used = new Histogram(target);

        int size = word.Length;
        for (int i = 0; i < size; ++i)
        {
            char letter = word[i];
            if (target[i] == letter)
            {
                clues[i] = Clue.Correct;
                used.Use(letter);
            }
        }

        for (int i = 0; i < size; ++i)
        {
            if (clues[i] == Clue.NotSet)
            {
                char letter = word[i];
                if (used.Has(letter))
                {
                    clues[i] = Clue.Used;
                    used.Use(letter);
                }
                else
                {
                    clues[i] = Clue.Unused;
                }
            }
        }
    }
}
