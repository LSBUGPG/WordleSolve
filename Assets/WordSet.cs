using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSet
{
    List<int> set;
    bool full = false;
    int count;

    public WordSet(int count, bool state)
    {
        full = state;
        if (full)
        {
            this.count = count;
        }
        else
        {
            set = new List<int>();
        }
    }

    public void Intersect(WordSet intersect)
    {
        if (full)
        {
            set = intersect.set;
            full = false;
        }
        else
        {
            int myIndex = 0;
            int intersectIndex = 0;

            List<int> intersection = new List<int>();
            while (myIndex < set.Count && intersectIndex < intersect.set.Count)
            {
                if (set[myIndex] < intersect.set[intersectIndex])
                {
                    myIndex++;
                }
                else if (intersect.set[intersectIndex] < set[myIndex])
                {
                    intersectIndex++;
                }
                else
                {
                    intersection.Add(set[myIndex]);
                    myIndex++;
                    intersectIndex++;
                }
            }
            set = intersection;
        }
    }

    public void Add(int index)
    {
        set.Add(index);
    }

    public int Count()
    {
        int count = full? this.count : set.Count;
        return count;
    }
}
