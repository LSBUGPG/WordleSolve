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
    int [] used = new int [26];

    public Match(string word)
    {
        this.word = word;
        clues = new Clue [word.Length];
    }

    public void Set(string start)
    {
        this.word = start;
        for (int i = 0; i < clues.Length; ++i)
        {
            clues[i] = Clue.NotSet;
        }
    }

    public void Score(string target)
    {
        for (int i = 0; i < used.Length; ++i)
        {
            used[i] = 0;
        }
        int size = word.Length;
        for (int i = 0; i < size; ++i)
        {
            used[target[i] - 'a']++;
        }

        for (int i = 0; i < size; ++i)
        {
            char letter = word[i];
            if (target[i] == letter)
            {
                clues[i] = Clue.Correct;
                used[letter - 'a']--;
            }
        }

        for (int i = 0; i < size; ++i)
        {
            if (clues[i] == Clue.NotSet)
            {
                char letter = word[i];
                if (used[letter - 'a'] > 0)
                {
                    clues[i] = Clue.Used;
                    used[letter - 'a']--;
                }
                else
                {
                    clues[i] = Clue.Unused;
                }
            }
        }
    }

    public bool CanReject(string candidate)
    {
        bool reject = false;
        for (int i = 0; i < used.Length; ++i)
        {
            used[i] = 0;
        }

        for (int i = 0; i < word.Length; ++i)
        {
            switch (clues[i])
            {
                case Clue.Correct:
                case Clue.Used:
                    used[word[i] - 'a']++;
                    break;
            }
        }

        for (int i = 0; i < word.Length && !reject; ++i)
        {
            switch (clues[i])
            {
                case Clue.Correct:
                    if (used[word[i] - 'a'] >= 0 && word[i] == candidate[i])
                    {
                        used[word[i] - 'a']--;
                    }
                    else
                    {
                        reject = true;
                    }
                    break;
                case Clue.Used:
                    if (used[word[i] - 'a'] >= 0 && word[i] != candidate[i])
                    {
                        used[word[i] - 'a']--;
                    }
                    else
                    {
                        reject = true;
                    }
                    break;
            }
        }

        for (int i = 0; i < word.Length && !reject; ++i)
        {
            switch (clues[i])
            {
                case Clue.Unused:
                    if (used[word[i] - 'a'] > 0)
                    {
                        reject = true;
                    }
                    break;
            }
        }

        return reject;
    }
}
