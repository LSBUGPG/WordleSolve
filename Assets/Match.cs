using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    string target;

    public Match(string target)
    {
        this.target = target;
    }

    public string Compare(string test)
    {
        Histogram histogram = new Histogram(target);
        char[] result = new char[target.Length];
        for (int i = 0; i < target.Length; ++i)
        {
            if (target[i] == test[i])
            {
                result[i] = 'm';
            }
            else if (histogram.LetterCount(test[i]) > 0)
            {
                result[i] = 'c';
                histogram.UseLetter(test[i]);
            }
            else
            {
                result[i] = 'n';
            }
        }
        return new string(result);
    }
}
