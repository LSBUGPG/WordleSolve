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
        char[] result = new char[target.Length];
        for (int i = 0; i < target.Length; ++i)
        {
            if (target[i] == test[i])
            {
                result[i] = 'm';
            }
            else if (target.Contains(test[i].ToString()))
            {
                result[i] = 'c';
            }
            else
            {
                result[i] = 'n';
            }
        }
        return new string(result);
    }
}
