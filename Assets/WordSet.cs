using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSet
{
    Bits bits;
    int count;

    public WordSet(int count, bool state)
    {
        bits = new Bits(count, state);
        this.count = state? count : 0;
    }

    public void Intersect(WordSet intersect)
    {
        count -= bits.Intersect(intersect.bits);
    }

    public void Add(int index)
    {
        if (bits.SetBit(index))
        {
            count++;
        }
    }

    public int Count()
    {
        return count;
    }
}
