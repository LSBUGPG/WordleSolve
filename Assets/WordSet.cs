using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSet
{
    int [] set;
    bool full = false;
    int count;

    public WordSet(int count, bool state)
    {
        full = state;
        set = new int [count];
        if (full)
        {
            this.count = count;
        }
        else
        {
            this.count = 0;
        }
    }

    public void Intersect(WordSet intersect)
    {
        if (full)
        {
            for (int i = 0; i < intersect.count; ++i)
            {
                set[i] = intersect.set[i];
            }
            count = intersect.count;
            full = false;
        }
        else
        {
            int myIndex = 0;
            int myNextIndex = 0;
            int intersectIndex = 0;

            while (myNextIndex < count && intersectIndex < intersect.count)
            {
                if (set[myNextIndex] < intersect.set[intersectIndex])
                {
                    myNextIndex++;
                }
                else if (intersect.set[intersectIndex] < set[myNextIndex])
                {
                    intersectIndex++;
                }
                else
                {
                    set[myIndex] = set[myNextIndex];
                    myIndex++;
                    myNextIndex++;
                    intersectIndex++;
                }
            }
            count = myIndex;
        }
    }

    public void Add(int index)
    {
        set[count] = index;
        count++;
    }

    public int Count()
    {
        return count;
    }

    public void Fill()
    {
        full = true;
        count = set.Length;
    }
}
