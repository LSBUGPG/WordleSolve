using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSet
{
    bool [] set;
    int count;

    public WordSet(int count, bool state)
    {
        set = new bool [count];
        int i;
        for (i = 0; i < count; ++i)
        {
            set[i] = state;
        }
        this.count = state? count : 0;
    }

    public void Intersect(WordSet intersect)
    {
        int size = set.Length;
        for (int i = 0; i < size; ++i)
        {
            bool removed = set[i] && !intersect.set[i];
            if (removed)
            {
                count--;
                set[i] = intersect.set[i];
            }
        }
    }

    public void Add(int index)
    {
        if (!set[index])
        {
            set[index] = true;
            count++;
        }
    }

    public int Count()
    {
        return count;
    }
}
